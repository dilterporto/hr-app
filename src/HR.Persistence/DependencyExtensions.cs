using HR.Abstractions.EventSourcing;
using HR.Domain.Aggregates.Departments;
using HR.Domain.Aggregates.Employees;
using HR.Persistence.Reading;
using HR.Persistence.Writing;
using HR.Persistence.Writing.EventCommiters;
using HR.Persistence.Writing.Events;
using HR.Persistence.Writing.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Persistence;

public static class DependencyExtensions
{
  public static IServiceCollection ConfigurePersistence(this IServiceCollection services, IConfiguration configuration)
  {
    services.AddDbContext<EventsDbContext>(o =>
      o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    
    services.AddDbContext<ProjectionsDbContext>(o =>
      o.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
    
    services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    services.AddScoped<IDepartmentRepository, DepartmentRepository>();
    services.AddScoped<IEventsCommiter, EventsCommitter>();
    services.AddScoped<IEventCommitter<EmployeeCreatedEvent>, EmployeeCreatedEventCommitter>();
    services.AddScoped<IEventCommitter<EmployeeChangedEvent>, EmployeeChangedEventCommitter>();
    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkPipelineBehavior<,>));
    services.AddScoped<IProjectionsReader, ProjectionsReader>();
    return services;
  }
}
