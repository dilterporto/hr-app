using HR.Abstractions.DDD;
using HR.Domain.Shared.ValueObjects;

namespace HR.Domain.Aggregates.Employees;

public class EmployeeCreatedEvent : DomainEvent
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public DateTime HireDate { get; set; }
  public Guid DepartmentId { get; set; }
  public Phone? PhoneNumber { get; set; }
  public Address? Address { get; set; }
  
  public EmployeeCreatedEvent() 
    => this.Id = Guid.CreateVersion7();
}
