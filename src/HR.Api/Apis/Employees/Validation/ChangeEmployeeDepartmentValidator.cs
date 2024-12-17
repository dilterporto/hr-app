using FluentValidation;
using HR.Employee.Api.Apis.Employees.Messages;

namespace HR.Employee.Api.Apis.Employees.Validation;

public class ChangeEmployeeDepartmentValidator : AbstractValidator<ChangeEmployeeDepartmentRequest>
{
  public ChangeEmployeeDepartmentValidator()
  {
    RuleFor(x => x.EmployeeId)
      .NotEmpty()
      .WithMessage("EmployeeId is required");

    RuleFor(x => x.DepartmentId)
      .NotEmpty()
      .WithMessage("DepartmentId is required");
  }
}
