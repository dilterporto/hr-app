using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Employees;
using HR.Persistence.Reading;
using HR.Persistence.Reading.Projections;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.EventCommiters;

public class EmployeeDeletedEventCommitter(ProjectionsDbContext projectionsDbContext, ILogger<EmployeeDeletedEventCommitter> logger) : IEventCommitter<EmployeeDeletedEvent>
{
  public async Task CommitAsync(EmployeeDeletedEvent @event)
  {
    var employeeProjection = await projectionsDbContext
      .Set<EmployeeProjection>()
      .FindAsync(@event.AggregateId);
    if (employeeProjection == null)
    {
      logger.LogWarning("[Persistence] Employee projection with id {Id} not found", @event.AggregateId);
      return;
    }

    projectionsDbContext.Remove(employeeProjection);
    await projectionsDbContext.SaveChangesAsync();
    
    logger.LogInformation("[Persistence] Employee projection with id {Id} deleted", @event.AggregateId);
  }
}
