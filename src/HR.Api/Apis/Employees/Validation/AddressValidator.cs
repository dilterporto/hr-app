using FluentValidation;
using HR.Domain.Shared.ValueObjects;

namespace HR.Employee.Api.Apis.Employees.Validation;

public class AddressValidator : AbstractValidator<Address>
{
  public AddressValidator()
  {
    RuleFor(x => x.Line1)
      .NotEmpty()
      .WithMessage("AddressLine1 is required");

    RuleFor(x => x.City)
      .NotEmpty()
      .WithMessage("City is required");

    RuleFor(x => x.State)
      .NotEmpty()
      .Length(2)
      .WithMessage("State is required");

    RuleFor(x => x.ZipCode)
      .NotEmpty()
      .Length(5)
      .WithMessage("ZipCode is required")
      .Matches(@"^\d{5}(?:[-\s]\d{4})?$")
      .WithMessage("ZipCode is invalid");
  }
}
