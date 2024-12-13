using AutoMapper;
using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Application.Contracts;
using HR.Domain.Aggregates.Departments;
using HR.Domain.Aggregates.Employees;
using HR.Domain.Shared.ValueObjects;

namespace HR.Application.UseCases.CreateEmployee;

public class CreateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper, IEmployeeFactory employeeFactory) 
  : ICommandHandler<CreateEmployeeCommandHandler.Command, Result<EmployeeResponse>>
{
  public record Command(
    string? FirstName,
    string? LastName,
    DateTime HireDate,
    Guid DepartmentId,
    Phone? Phone,
    Address? Address) : ICommand<Result<EmployeeResponse>>
  {
    public Command() : this(null, null, default, Guid.Empty, null, null)
    {
    }
  }
  
  public async Task<Result<EmployeeResponse>> Handle(Command command, CancellationToken cancellationToken)
  {
    // var department = new DepartmentState() { Name = "Department 1", };
    //   
    // await departmentRepository.SaveAsync(new Department(department));
    
    return await employeeFactory
      .CreateAsync(mapper.Map<EmployeeState>(command))
      .ToResult("Error creating employee")
      .Check(repository.SaveAsync)
      .Map(employee => mapper.Map<EmployeeResponse>(employee.State));
  }
}
