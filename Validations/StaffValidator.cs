using FluentValidation;
using WebApplication1.Controllers;

namespace WebApplication1.Validations
{
    public class StaffValidator : AbstractValidator<Staff>
    {
        public StaffValidator()
        {
            RuleFor(staff => staff.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(10, 250).WithMessage("Name length must be between 10 and 250 characters.");

            RuleFor(staff => staff.Email)
                .EmailAddress().WithMessage("Email address is not valid.");

            RuleFor(staff => staff.Phone)
                .NotEmpty().WithMessage("Phone number is required.");

            RuleFor(staff => staff.HourlySalary)
                .NotNull().WithMessage("Hourly salary is required.")
                .InclusiveBetween(30, 400).WithMessage("Hourly salary must be between 30 and 400.");
        }
    }
}
