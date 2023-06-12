using Serilog;
using Zibma.Sample.Person.Api;
using Zibma.Sample.Person.Api.Common.Helpers;
using Zibma.Sample.Person.Api.Common.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddServices();
builder.Host.UseSerilog();

var app = builder.Build();

AppSettings appSettings = AppSettingHelper.GetSettings();

BLL.DBU.SetBaseConnectionString(appSettings.ConnectionStrings.Main.Trim());

#region Serilog

var configuration = new ConfigurationBuilder()
    .AddJsonStream(AppSettingHelper.GetStream())
    .Build();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .Enrich.WithProperty("Service", "MS-Biomatric")
    .Enrich.WithClientIp()
    .Enrich.WithClientAgent()
    .CreateLogger();

app.UseSerilogRequestLogging();

#endregion


// Configure the HTTP request pipeline.
if (appSettings.IsDevMode)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyCors");

app.UseAuthorization();

app.MapControllers();

try
{
    Log.Information("Application Starting");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Error in App Starting");
}
finally
{
    Log.CloseAndFlush();
}
