using FluentValidation;
using HR.Employee.Api.Apis.Departments.Messages;

namespace HR.Employee.Api.Apis.Departments.Validation;

public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentRequest>
{
  public CreateDepartmentValidator() =>
    RuleFor(x => x.Name)
      .NotEmpty()
      .WithMessage("Name is required");
}
