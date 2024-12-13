using HR.Domain.Shared.ValueObjects;

namespace HR.Employee.Api.Apis.Employees.Messages;

public class CreateEmployeeRequest
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public DateTime HireDate { get; set; }
  public Guid DepartmentId { get; set; }
  public string? PhoneNumber { get; set; }
  public Address? Address { get; set; }
}
