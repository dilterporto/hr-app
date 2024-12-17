using AutoMapper;
using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Application.Contracts;
using HR.Domain.Aggregates.Departments;
using HR.Domain.Aggregates.Employees;

namespace HR.Application.UseCases.ChangeEmployeeDepartment;

public class ChangeEmployeeDepartmentCommandHandler(
  IEmployeeRepository repository, 
  IDepartmentRepository departmentRepository, 
  IMapper mapper) 
  : ICommandHandler<ChangeEmployeeDepartmentCommandHandler.Command, Result<EmployeeResponse>>
{
  public record Command(Guid EmployeeId, Guid DepartmentId) : ICommand<Result<EmployeeResponse>>
  {
    public Command() : this(Guid.Empty, Guid.Empty)
    {
    }
  }

  public async Task<Result<EmployeeResponse>> Handle(Command request, CancellationToken cancellationToken) =>
    await repository.LoadByIdAsync(request.EmployeeId)
      .ToResult($"Employee with id {request.EmployeeId} not found")
      .Ensure(_ => departmentRepository.LoadByIdAsync(request.DepartmentId).Result.HasValue, "Department not found")
      .Tap(employee => employee.ChangeDepartment(request.DepartmentId))
      .Check(repository.SaveAsync)
      .Map(employee => mapper.Map<EmployeeResponse>(employee.State));
}
