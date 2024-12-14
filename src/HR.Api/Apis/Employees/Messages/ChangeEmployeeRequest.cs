namespace HR.Employee.Api.Apis.Employees.Messages;

public class ChangeEmployeeRequest
{
  public Guid EmployeeId { get; set; }
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? PhoneNumber { get; set; }
}
