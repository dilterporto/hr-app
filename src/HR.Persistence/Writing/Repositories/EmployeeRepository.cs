using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Employees;
using HR.Persistence.Writing.Events;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.Repositories;

public class EmployeeRepository(EventsDbContext dbContext, IEventsCommiter iEventsCommiter, ILogger<IEmployeeRepository> logger) 
  : Repository<Employee, IEmployeeRepository>(dbContext, iEventsCommiter, logger), IEmployeeRepository
{
  
}
