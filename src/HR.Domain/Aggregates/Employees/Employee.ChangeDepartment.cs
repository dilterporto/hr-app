namespace HR.Domain.Aggregates.Employees;

public partial class Employee
{
  public void ChangeDepartment(Guid departmentId)
   => ApplyChange(new EmployeeDepartmentChangedEvent
   {
     DepartmentId = departmentId
   });
  
  public void Apply(EmployeeDepartmentChangedEvent @event)
    => this.State!.DepartmentId = @event.DepartmentId;
}
