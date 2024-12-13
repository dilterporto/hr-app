using CSharpFunctionalExtensions;

namespace HR.Domain.Aggregates.Departments;

public interface IDepartmentRepository
{
  /// <summary>
  /// 
  /// </summary>
  /// <param name="id"></param>
  /// <returns></returns>
  Task<Maybe<Department>> LoadByIdAsync(Guid id);
  
  /// <summary>
  /// 
  /// </summary>
  /// <param name="department"></param>
  /// <returns></returns>
  Task<Result> SaveAsync(Department department);
}
