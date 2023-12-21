using FluentValidation;
using WebApplication1.Controllers;

public class EmployeeValidator : AbstractValidator<Employee>
{
    public EmployeeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(250)
            .WithMessage("Invalid Name");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .Must(BeValidBirthdate)
            .WithMessage("Birthdate is not valid.");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email address is not valid.");

        RuleFor(x => x.Phone).NotEmpty().WithMessage("Phone");
          
        RuleFor(x => x.HourlySalary)
            .InclusiveBetween(50, 400)
            .WithMessage("Hourly salary does not fall within allowed range.")
            .When(x => x.DateOfBirth <= DateTime.Today.AddYears(-30))
            .Must((employee, hourlySalary) => BeValidSalary(employee, hourlySalary))
            .WithMessage("Minimum hourly salary is not valid.");
    }

    private bool BeValidBirthdate(DateTime dateOfBirth)
    {
        var minAllowedBirthDate = DateTime.Today.AddYears(-65);
        return minAllowedBirthDate <= dateOfBirth;
    }

    private bool BeValidSalary(Employee employee, double hourlySalary)
    {
        var dateBeforeThirtyYears = DateTime.Today.AddYears(-30);
        var isOlderThanThirtyYears = employee.DateOfBirth <= dateBeforeThirtyYears;

        return isOlderThanThirtyYears ? hourlySalary >= 200 : hourlySalary >= 50;
    }
}
