using CSharpFunctionalExtensions;
using HR.Domain.Aggregates.Departments;
using HR.Domain.Aggregates.Employees;

namespace HR.Application.UseCases.CreateEmployee.Factories;

public class EmployeeFactory(IDepartmentRepository departmentRepository) : IEmployeeFactory
{
  public async Task<Maybe<Employee>> CreateAsync(EmployeeState state)
  {
    var department = await departmentRepository.LoadByIdAsync(state.DepartmentId);
    return department.HasNoValue ? 
      Maybe<Employee>.None : new Employee(state);
  }
}
