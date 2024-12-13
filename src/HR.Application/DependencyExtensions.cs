using System.Reflection;
using HR.Abstractions.Logging;
using HR.Application.UseCases.CreateEmployee;
using HR.Application.UseCases.CreateEmployee.Factories;
using HR.Domain.Aggregates.Employees;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace HR.Application;

public static class DependencyExtensions
{
  public static IServiceCollection ConfigureApplication(this IServiceCollection services)
  {
    services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(CreateEmployeeCommandHandler).Assembly);
    
    services.AddScoped<IEmployeeFactory, EmployeeFactory>();
    
    return services;
  }
}
