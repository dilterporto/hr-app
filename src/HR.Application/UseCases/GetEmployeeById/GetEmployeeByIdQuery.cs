using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Application.Contracts;

namespace HR.Application.UseCases.GetEmployeeById;

public class GetEmployeeByIdQuery : IQuery<Result<EmployeeResponse>>
{
  public Guid Id { get; set; }
}
