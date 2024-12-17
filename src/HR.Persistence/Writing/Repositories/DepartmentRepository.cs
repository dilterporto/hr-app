using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Departments;
using HR.Persistence.Writing.Events;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.Repositories;

public class DepartmentRepository : Repository<Department, IDepartmentRepository>, IDepartmentRepository
{
  public DepartmentRepository(
    EventsDbContext dbContext, 
    IEventsCommiter eventsCommitter, 
    ILogger<IDepartmentRepository> logger,
    // events
    IEventCommitter<DepartmentCreatedEvent> departmentCreatedEventCommitter) 
    : base(dbContext, eventsCommitter, logger)
  {
    eventsCommitter.AddAsync(departmentCreatedEventCommitter);
  }
}
