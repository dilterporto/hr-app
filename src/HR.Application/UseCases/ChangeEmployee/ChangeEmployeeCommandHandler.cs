using AutoMapper;
using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Application.Contracts;
using HR.Domain.Aggregates.Employees;
using HR.Domain.Shared.ValueObjects;

namespace HR.Application.UseCases.ChangeEmployee;

public class ChangeEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper) : ICommandHandler<ChangeEmployeeCommandHandler.Command, Result<EmployeeResponse>>
{
  public record Command(
    Guid EmployeeId, 
    string FirstName, 
    string LastName, 
    Phone PhoneNumber) 
    : ICommand<Result<EmployeeResponse>>
  {
    public Command() : this(Guid.Empty, string.Empty, string.Empty, null!)
    {
    }
  }

  public async Task<Result<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken) =>
    await repository.LoadByIdAsync(request.EmployeeId)
      .ToResult($"Employee with id {request.EmployeeId} not found")
      .Tap(employee => employee.Change(request.FirstName, request.LastName, request.PhoneNumber))
      .Check(repository.SaveAsync)
      .Map(employee => mapper.Map<EmployeeResponse>(employee.State));
}
