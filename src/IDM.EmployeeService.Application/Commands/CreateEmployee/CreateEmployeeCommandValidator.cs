using FluentValidation;
using System.Text.RegularExpressions;

namespace IDM.EmployeeService.Application.Commands.CreateEmployee
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeCommand>
    {
        public CreateEmployeeCommandValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Employee first name is empty")
                .MinimumLength(2).WithMessage("Employee first name is empty")
                .MaximumLength(50).WithMessage("Employee first name is empty");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Employee last name is empty")
                .MaximumLength(50).WithMessage("Employee first name is empty");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Employee position is empty")
                .MaximumLength(50).WithMessage("Employee first name is empty");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Employee email is empty")
                .MaximumLength(50).WithMessage("Employee first name is empty")
                .Must(x => Regex.IsMatch(x, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")).WithMessage("Employee email is invalid");
        }
    }
}
