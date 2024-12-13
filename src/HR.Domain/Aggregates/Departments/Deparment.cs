using HR.Abstractions.DDD;

namespace HR.Domain.Aggregates.Departments;

public class Department : AggregateRoot, IDepartmentAggregate
{
  public DepartmentState State { get; } = new();

  public Department() { }
  
  public Department(DepartmentState state) : base(state.Id)
    => ApplyChange(new DepartmentCreatedEvent
    {
      Name = state.Name
    });

  public void Apply(DepartmentCreatedEvent @event) 
    => this.State.Name = @event.Name;
}
