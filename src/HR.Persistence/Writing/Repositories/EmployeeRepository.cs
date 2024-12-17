using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Employees;
using HR.Persistence.Writing.Events;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.Repositories;

public class EmployeeRepository : Repository<Employee, IEmployeeRepository>, IEmployeeRepository
{
  public EmployeeRepository(
    EventsDbContext dbContext, 
    IEventsCommiter eventsCommitter, 
    ILogger<IEmployeeRepository> logger,
    // events
    IEventCommitter<EmployeeCreatedEvent> employeeCreatedEventCommitter,
    IEventCommitter<EmployeeChangedEvent> employeeChangedEventCommitter,
    IEventCommitter<EmployeeDeletedEvent> employeeDeletedEventCommitter,
    IEventCommitter<EmployeeDepartmentChangedEvent> employeeDepartmentChangedEventCommitter) 
    : base(dbContext, eventsCommitter, logger)
  {
    eventsCommitter.AddAsync(employeeCreatedEventCommitter);
    eventsCommitter.AddAsync(employeeChangedEventCommitter);
    eventsCommitter.AddAsync(employeeDeletedEventCommitter);
    eventsCommitter.AddAsync(employeeDepartmentChangedEventCommitter);
  }
}
