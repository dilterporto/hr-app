using HR.Abstractions.DDD;

namespace HR.Domain.Aggregates.Employees;

public class EmployeeDeletedEvent : DomainEvent
{
  public EmployeeDeletedEvent() 
    => this.Id = Guid.CreateVersion7();
}