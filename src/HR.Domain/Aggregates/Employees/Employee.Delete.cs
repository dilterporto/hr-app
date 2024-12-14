namespace HR.Domain.Aggregates.Employees;

public partial class Employee
{
  public void Delete(string reason)
    => ApplyChange(new EmployeeDeletedEvent());
  
  public void Apply(EmployeeDeletedEvent _)
    => this.State!.IsDeleted = true;
}
