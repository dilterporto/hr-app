using HR.Abstractions.DDD;

namespace HR.Domain.Aggregates.Employees;

public partial class Employee : AggregateRoot, IEmployeeAggregate
{
  public EmployeeState State { get; } = new();
  public Employee() { }
  public Employee(EmployeeState state) : base(state.Id)
    => ApplyChange(new EmployeeCreatedEvent
    {
      Id = state.Id,
      FirstName = state.FirstName,
      LastName = state.LastName,
      HireDate = state.HireDate,
      DepartmentId = state.DepartmentId,
      PhoneNumber = state.PhoneNumber,
      Address = state.Address
    });

  public void Apply(EmployeeCreatedEvent @event)
  {
    this.State.FirstName = @event.FirstName;
    this.State.LastName = @event.LastName;
    this.State.DepartmentId = @event.DepartmentId;
    this.State.HireDate = @event.HireDate;
    this.State.PhoneNumber = @event.PhoneNumber;
    this.State.Address = @event.Address;
  }
}
