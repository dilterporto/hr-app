using HR.Abstractions.EventSourcing;

namespace HR.Persistence.Reading.Projections;

public class DepartmentProjection : Projection
{
  public string? Name { get; set; }
}
