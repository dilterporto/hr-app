using HR.Abstractions.DDD;

namespace HR.Domain.Aggregates.Departments;

public class DepartmentCreatedEvent : DomainEvent
{
  public string? Name { get; set; }
  
  public DepartmentCreatedEvent() 
    => this.Id = Guid.CreateVersion7();
}
