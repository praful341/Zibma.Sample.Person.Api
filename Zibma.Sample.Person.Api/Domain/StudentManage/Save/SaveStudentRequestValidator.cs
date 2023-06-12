using FluentValidation;
using Zibma.MS.Common.Helpers;
using Zibma.Sample.Person.Api.Domain.PersonManage.Save;

namespace Zibma.Sample.Person.Api.Domain.StudentManage.Save
{
    public class SaveStudentRequestValidator : AbstractValidator<SaveStudentRequestModel>
    {
        public SaveStudentRequestValidator()
        {
            RuleFor(x => x.StudentId).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(t => t.StudentName).NotNull().NotEmpty().WithMessage("{PropertyName} should not be empty or null.")
                                    .MaximumLength(50).WithMessage("The length of '{PropertyName}' must be {MaxLength} characters");
            RuleFor(t => t.Mobile).Length(10).WithMessage("Enter Valid '{PropertyName}'");

            RuleFor(t => t.CityName).NotNull().NotEmpty().WithMessage("{PropertyName} should not be empty or null.");

            When(t => !string.IsNullOrEmpty(t.EmailAddress), () =>
            {
                RuleFor(t => t.EmailAddress).Must(ValidationHelper.IsEmail).WithMessage("Please Enter Valid '{PropertyName}'")
                                        .MaximumLength(50).WithMessage("The length of '{PropertyName}' must be {MaxLength} characters");
            });

            RuleFor(t => t.SchoolFees).NotNull().GreaterThan(0);
            RuleFor(t => t.BusFees).NotNull().GreaterThan(0);

            RuleFor(t => t.Gender).IsInEnum().WithMessage("{PropertyName} is Invalid.");
        }
    }
}
