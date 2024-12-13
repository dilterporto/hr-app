using HR.Abstractions.DDD;

namespace HR.Domain.Aggregates.Departments;

public interface IDepartmentAggregate : IAggregateRoot
{
  DepartmentState? State { get; }
}
