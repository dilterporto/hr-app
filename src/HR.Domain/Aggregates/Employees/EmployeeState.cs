using HR.Domain.Shared.ValueObjects;

namespace HR.Domain.Aggregates.Employees;

public class EmployeeState
{
  public Guid Id { get; set; } = Guid.CreateVersion7();
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public DateTime HireDate { get; set; }
  public Guid DepartmentId { get; set; }
  public Phone? PhoneNumber { get; set; }
  public Address? Address { get; set; }
  public bool IsDeleted { get; set; } 
}
