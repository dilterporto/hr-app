using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Departments;
using HR.Persistence.Reading;
using HR.Persistence.Reading.Projections;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.EventCommiters;

public class DepartmentCreatedEventCommitter(ProjectionsDbContext projectionsDbContext, ILogger<DepartmentCreatedEventCommitter> logger) 
  : IEventCommitter<DepartmentCreatedEvent>
{
  public async Task CommitAsync(DepartmentCreatedEvent @event)
  {
    var departmentProjection = new DepartmentProjection
    {
      Id = @event.AggregateId,
      Name = @event.Name
    };

    await projectionsDbContext.AddAsync(departmentProjection);
    await projectionsDbContext.SaveChangesAsync();
    
    logger.LogInformation("[Persistence] Department projection with id {Id} created {Projection}", @event.AggregateId, departmentProjection);
  }
}
