using CSharpFunctionalExtensions;

namespace HR.Domain.Aggregates.Employees;

public interface IEmployeeRepository
{
  /// <summary>
  /// Load employee by id
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<Maybe<Employee>> LoadByIdAsync(Guid id);
  
  /// <summary>
  /// Save employee state
  /// </summary>
  /// <param name="employee"></param>
  /// <returns></returns>
  Task<Result> SaveAsync(Employee employee);
}
