using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Employees;
using HR.Persistence.Reading;
using HR.Persistence.Reading.Projections;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.EventCommiters;

public class EmployeeChangedEventCommitter(ProjectionsDbContext projectionsDbContext, ILogger<EmployeeChangedEventCommitter> logger) 
  : IEventCommitter<EmployeeChangedEvent>
{
  public async Task CommitAsync(EmployeeChangedEvent @event)
  {
    var employeeProjection = await projectionsDbContext.Set<EmployeeProjection>().FindAsync(@event.AggregateId);
    if (employeeProjection == null)
    {
      logger.LogError("[Persistence] Employee with id {Id} not found", @event.AggregateId);
      return;
    }
    
    employeeProjection.FirstName = @event.FirstName!;
    employeeProjection.LastName = @event.LastName!;
    employeeProjection.PhoneNumber = $"{@event.PhoneNumber!.Number}";
    
    await projectionsDbContext.SaveChangesAsync();
    logger.LogInformation("[Persistence] Employee projection with id {Id} updated {Projection}", @event.AggregateId, employeeProjection);
  }
}
