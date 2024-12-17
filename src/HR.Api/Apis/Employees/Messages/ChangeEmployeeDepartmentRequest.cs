namespace HR.Employee.Api.Apis.Employees.Messages;

public class ChangeEmployeeDepartmentRequest
{
  public Guid EmployeeId { get; set; }
  public Guid DepartmentId { get; set; }
}
