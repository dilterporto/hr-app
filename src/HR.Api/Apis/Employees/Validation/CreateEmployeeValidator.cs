using FluentValidation;
using HR.Employee.Api.Apis.Employees.Messages;

namespace HR.Employee.Api.Apis.Employees.Validation;

public class CreateEmployeeValidator : AbstractValidator<CreateEmployeeRequest>
{
  public CreateEmployeeValidator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .WithMessage("FirstName is required");

    RuleFor(x => x.LastName)
      .NotEmpty()
      .WithMessage("LastName is required");
    
    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .WithMessage("PhoneNumber is required")
      .Matches(@"^\+[1-9]\d{1,14}$")
      .WithMessage("PhoneNumber is invalid");

    RuleFor(x => x.DepartmentId)
      .NotEmpty()
      .WithMessage("DepartmentId is required");

    RuleFor(x => x.Address)
      .SetValidator(new AddressValidator()!);
  }
}
