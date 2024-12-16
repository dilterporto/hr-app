using System.Reflection;
using FastEndpoints;
using FastEndpoints.Swagger;
using HR.Application.UseCases.CreateEmployee;

namespace HR.Employee.Api;

public static class DependencyConfig
{
  public static IServiceCollection ConfigureApis(this IServiceCollection services)
  {
    services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(CreateEmployeeCommandHandler).Assembly);
    services
      .AddFastEndpoints()
      .SwaggerDocument(o =>
      {
        o.DocumentSettings = s =>
        {
          s.Title = "Human Resource APIs";
          s.Version = "v0";
        };
        o.AutoTagPathSegmentIndex = 0;
      });

    return services;
  }
}
