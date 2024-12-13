using CSharpFunctionalExtensions;

namespace HR.Domain.Aggregates.Employees;

public interface IEmployeeFactory
{
  Task<Maybe<Employee>> CreateAsync(EmployeeState state);
}
