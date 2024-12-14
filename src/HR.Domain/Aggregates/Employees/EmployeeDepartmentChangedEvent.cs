using HR.Abstractions.DDD;

namespace HR.Domain.Aggregates.Employees;

public class EmployeeDepartmentChangedEvent : DomainEvent
{
  public Guid DepartmentId { get; set; }
  
  public EmployeeDepartmentChangedEvent() 
    => this.Id = Guid.CreateVersion7();
}