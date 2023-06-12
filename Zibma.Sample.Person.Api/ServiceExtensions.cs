using FluentValidation.AspNetCore;
using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using Zibma.Sample.Person.Api.Common.Helpers;
using Zibma.Sample.Person.Api.Common.Models;
using Zibma.Sample.Person.Api.Common.ActionFilters;

namespace Zibma.Sample.Person.Api
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {

            AppSettings appSettings = AppSettingHelper.GetSettings();

            services.AddCors(options =>
            {
                options.AddPolicy("MyCors",
                    builder =>
                    {
                        builder.WithOrigins("*")
                            .AllowAnyHeader()
                            .WithMethods("GET", "POST", "PUT", "DELETE");
                    });
            });

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                })
                .AddJsonOptions(jsonOptions => { })
                .ConfigureApiBehaviorOptions(x => { x.SuppressModelStateInvalidFilter = true; }
                );


            services.AddJwtAuthentication(appSettings);
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();

            services.AddHttpContextAccessor();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapperExtension();
            services.AddMassTransitExtension(appSettings);

            return services;
        }


        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddAuthorization();
            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(token =>
            {
                token.RequireHttpsMetadata = true;
                token.SaveToken = true;
                token.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.AccessToken?.SecretKey ?? "")),
                    //ValidateIssuer = true,
                    //ValidIssuer = appSettings.AccessToken?.Issuer,
                    //ValidateAudience = true,
                    //ValidAudience = appSettings.AccessToken?.Issuer,
                    RequireExpirationTime = true,
                    ValidateLifetime = false,
                    ClockSkew = TimeSpan.Zero
                };
            });


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API List",
                    Description = "All API List for the project"
                });
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT authorization header using bearer scheme",
                    Name = "Authorization",
                    @In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
                    {
                        new OpenApiSecurityScheme {
                            Reference = new OpenApiReference {
                                    Type = ReferenceType.SecurityScheme,
                                        Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                        },
                        new List < string > ()
                    }
                    });

                // Set the comments path for the Swagger JSON and UI.
                //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                // options.IncludeXmlComments(xmlPath);
            });


            return services;
        }

        public static void AddAutoMapperExtension(this IServiceCollection services)
        {
            services.AddAutoMapper((config) =>
            {
                config.AddProfile<MappingProfile>();
            });
        }

        public static void AddMassTransitExtension(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddMassTransit(x =>
            {
                #region Consumers Adding
                //x.AddConsumer<SchoolLogoUpdateConsumer>();
                #endregion


                x.UsingRabbitMq((ctx, cfg) =>
                {
                    #region RabbitMq Configuration
                    cfg.Host(new Uri(appSettings.RabbitMq.HostUrl), h =>
                    {
                        h.Username(appSettings.RabbitMq.Username);
                        h.Password(appSettings.RabbitMq.Password);
                    });
                    #endregion

                    #region Consumers Mapping

                    //cfg.ReceiveEndpoint("school-logo-update-event", e =>
                    //{
                    //    e.Consumer<SchoolLogoUpdateConsumer>();
                    //});


                    #endregion

                });
            });
        }
    }
}
