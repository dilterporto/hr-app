using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Employees;
using HR.Persistence.Reading;
using HR.Persistence.Reading.Projections;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.EventCommiters;

public class EmployeeDepartmentChangedEventCommitter(ProjectionsDbContext projectionsDbContext, ILogger<EmployeeDepartmentChangedEventCommitter> logger) 
  : IEventCommitter<EmployeeDepartmentChangedEvent>
{
  public async Task CommitAsync(EmployeeDepartmentChangedEvent @event)
  {
    var employeeProjection = await projectionsDbContext
      .Set<EmployeeProjection>()
      .FindAsync(@event.AggregateId);
    if (employeeProjection == null)
    {
      logger.LogWarning("[Persistence] Employee projection with id {Id} not found", @event.AggregateId);
      return;
    }

    employeeProjection.DepartmentId = @event.DepartmentId;
    await projectionsDbContext.SaveChangesAsync();
    
    logger.LogInformation("[Persistence] Employee projection with id {Id} department changed to {DepartmentId}", @event.AggregateId, @event.DepartmentId);
  }
}
