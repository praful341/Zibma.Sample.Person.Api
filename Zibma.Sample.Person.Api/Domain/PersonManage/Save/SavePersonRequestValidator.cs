using FluentValidation;
using Zibma.MS.Common.Helpers;

namespace Zibma.Sample.Person.Api.Domain.PersonManage.Save
{
    public class SavePersonRequestValidator : AbstractValidator<SavePersonRequestModel>
    {
        public SavePersonRequestValidator()
        {
            RuleFor(x => x.PersonId).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(t => t.FirstName).NotNull().NotEmpty().WithMessage("{PropertyName} should not be empty or null.")
                                    .MaximumLength(50).WithMessage("The length of '{PropertyName}' must be {MaxLength} characters");
            RuleFor(t => t.LastName).NotNull().NotEmpty().WithMessage("{PropertyName} should not be empty or null.")
                                    .MaximumLength(50).WithMessage("The length of '{PropertyName}' must be {MaxLength} characters");
            RuleFor(t => t.Mobile).Length(10).WithMessage("Enter Valid '{PropertyName}'");

            When(t => !string.IsNullOrEmpty(t.Email), () =>
            {
                RuleFor(t => t.Email).Must(ValidationHelper.IsEmail).WithMessage("Please Enter Valid '{PropertyName}'")
                                        .MaximumLength(50).WithMessage("The length of '{PropertyName}' must be {MaxLength} characters");
            });

            RuleFor(t => t.Gender).IsInEnum().WithMessage("{PropertyName} is Invalid.");
        }
    }
}
