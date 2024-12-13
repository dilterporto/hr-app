using HR.Abstractions.EventSourcing;

namespace HR.Persistence.Reading.Projections;

public class EmployeeProjection : Projection
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public DateTime HireDate { get; set; }
  public Guid DepartmentId { get; set; }
  public string? PhoneNumber { get; set; }
  public string? AddressLine1 { get; set; }
  public string? AddressLine2 { get; set; }
  public string? AddressCity { get; set; }
  public string? AddressState { get; set; }
  public string? AddressZipCode { get; set; }
}
