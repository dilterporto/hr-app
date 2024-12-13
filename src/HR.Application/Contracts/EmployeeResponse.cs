using HR.Domain.Shared.ValueObjects;

namespace HR.Application.Contracts;

public class EmployeeResponse
{
  public Guid Id { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public DateTime HireDate { get; set; }
  public Guid DepartmentId { get; set; }
  public string? PhoneNumber { get; set; }
  public Address? Address { get; set; }
}
