using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Employees;
using HR.Persistence.Reading;
using HR.Persistence.Reading.Projections;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.EventCommiters;

public class EmployeeCreatedEventCommitter(ProjectionsDbContext projectionsDbContext, ILogger<EmployeeCreatedEventCommitter> logger) : IEventCommitter<EmployeeCreatedEvent>
{
  public async Task CommitAsync(EmployeeCreatedEvent @event)
  {
    var employeeProjection = new EmployeeProjection
    {
      Id = @event.AggregateId,
      FirstName = @event.FirstName,
      LastName = @event.LastName,
      DepartmentId = @event.DepartmentId,
      PhoneNumber = $"{@event.PhoneNumber}",
      HireDate = @event.HireDate,
      AddressLine1 = @event.Address!.Line1,
      AddressLine2 = @event.Address.Line2,
      AddressCity = @event.Address.City,
      AddressState = @event.Address.State,
      AddressZipCode = @event.Address.ZipCode
    };

    await projectionsDbContext.AddAsync(employeeProjection);
    await projectionsDbContext.SaveChangesAsync();
    
    logger.LogInformation("[Persistence] Employee projection with id {Id} created {Projection}", @event.AggregateId, employeeProjection);
  }
}
