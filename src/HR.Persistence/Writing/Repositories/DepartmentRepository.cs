using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Departments;
using HR.Persistence.Writing.Events;
using Microsoft.Extensions.Logging;

namespace HR.Persistence.Writing.Repositories;

public class DepartmentRepository(EventsDbContext dbContext, IEventsCommiter iEventsCommiter, ILogger<IDepartmentRepository> logger) 
  : Repository<Department, IDepartmentRepository>(dbContext, iEventsCommiter, logger), IDepartmentRepository
{
  
}
