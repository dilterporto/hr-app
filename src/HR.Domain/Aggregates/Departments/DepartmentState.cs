namespace HR.Domain.Aggregates.Departments;

public class DepartmentState
{
  public Guid Id { get; set; } = Guid.CreateVersion7();  
  public string? Name { get; set; }

  public static implicit operator DepartmentState(string name)
    => new() { Name = name };
}
