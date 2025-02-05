using FluentValidation;
using FluentValidation.Validators;
using ExamTestAPI.DTO;

namespace ExamTestAPI.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(e => e.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible).WithMessage("Email is not valid");
            RuleFor(e => e.Email).Matches(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$").WithMessage("Email is not valid");
            RuleFor(e => e.Position).NotEmpty().WithMessage("Position is required");
            RuleFor(e => e.Salary).GreaterThan(-1).WithMessage("Salary must be positive value");
        }
    }
}
