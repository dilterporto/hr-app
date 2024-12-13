using System.Reflection;
using FastEndpoints;
using FastEndpoints.Swagger;
using HR.Application;
using HR.Application.UseCases.CreateEmployee;
using HR.Domain.Aggregates.Departments;
using HR.Persistence;
using HR.Persistence.Writing.Events;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly(), typeof(CreateEmployeeCommandHandler).Assembly);

builder.Services
  .AddFastEndpoints()
  .SwaggerDocument(o =>
  {
    o.DocumentSettings = s =>
    {
      s.Title = "Human Resource APIs";
      s.Version = "v0";
    };
  });

var app = builder.Build();

app
  .UseFastEndpoints()
  .UseSwaggerGen(o =>
  {

  });

app.Run();
