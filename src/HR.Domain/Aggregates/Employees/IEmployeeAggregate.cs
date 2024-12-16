using HR.Abstractions.DDD;
using HR.Domain.Shared.ValueObjects;

namespace HR.Domain.Aggregates.Employees;

/// <summary>
/// Employee aggregate root
/// </summary>
public interface IEmployeeAggregate : IAggregateRoot
{
  /// <summary>
  /// Holds employee state
  /// </summary>
  EmployeeState? State { get; }

  /// <summary>
  /// Change employee state
  /// </summary>
  void Change(string firstName, string lastName, Phone phoneNumber);
  
  /// <summary>
  /// Change department
  /// </summary>
  /// <param name="departmentId"></param>
  void ChangeDepartment(Guid departmentId);
  
  /// <summary>
  /// Delete employee
  /// </summary>
  void Delete();
}
