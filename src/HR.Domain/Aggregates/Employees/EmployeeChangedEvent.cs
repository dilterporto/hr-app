using HR.Abstractions.DDD;
using HR.Domain.Shared.ValueObjects;

namespace HR.Domain.Aggregates.Employees;

public class EmployeeChangedEvent : DomainEvent
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public Phone? PhoneNumber { get; set; }
  
  public EmployeeChangedEvent() 
    => this.Id = Guid.CreateVersion7();
}