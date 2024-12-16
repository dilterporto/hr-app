using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Domain.Aggregates.Employees;

namespace HR.Application.UseCases.DeleteEmployee;

public class DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository) 
  : ICommandHandler<DeleteEmployeeCommandHandler.Command, Result>
{
  public record Command(Guid EmployeeId) : ICommand<Result>;
  
  public async Task<Result> Handle(Command request, CancellationToken cancellationToken) =>
    await employeeRepository.LoadByIdAsync(request.EmployeeId)
      .ToResult($"Employee with id {request.EmployeeId} not found")
      .Tap(employee => employee.Delete())
      .Check(employeeRepository.SaveAsync);
}
