using FastEndpoints;
using FastEndpoints.Swagger;
using HR.Application;
using HR.Employee.Api;
using HR.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurePersistence(builder.Configuration);
builder.Services.ConfigureApplication();
builder.Services.ConfigureApis();

var app = builder.Build();

app
  .UseFastEndpoints()
  .UseSwaggerGen(o =>
  {

  });

app.Run();
