using HR.Domain.Shared.ValueObjects;

namespace HR.Domain.Aggregates.Employees;

public partial class Employee
{
  public void Change(string firstName, string lastName, Phone phoneNumber)
    => ApplyChange(new EmployeeChangedEvent
    {
      FirstName = firstName,
      LastName = lastName,
      PhoneNumber = phoneNumber
    });

  public void Apply(EmployeeChangedEvent @event)
  {
    this.State!.FirstName = @event.FirstName!;
    this.State.LastName = @event.LastName!;
    this.State.PhoneNumber = @event.PhoneNumber;
  }
}
