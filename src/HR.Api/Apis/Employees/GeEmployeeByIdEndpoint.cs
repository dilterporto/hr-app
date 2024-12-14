using CSharpFunctionalExtensions;
using FastEndpoints;
using HR.Application.Contracts;
using HR.Application.UseCases.GetEmployeeById;
using MediatR;

namespace HR.Employee.Api.Apis.Employees;

public class GeEmployeeByIdEndpoint(IMediator mediator) : EndpointWithoutRequest<EmployeeResponse>
{
  public override void Configure()
  {
    Get("api/employees/{id}");
    AllowAnonymous();
  }
  
  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var query = new GetEmployeeByIdQuery { Id = Guid.Parse(Route<string>("id")!) };
    
    var result = await mediator.Send(query, cancellationToken);

    await HandleResult(result, cancellationToken);
  }
  
  private Task HandleResult(Result<EmployeeResponse> result, CancellationToken cancellationToken) =>
    result.IsSuccess ? SendAsync(result.Value, cancellation: cancellationToken) : SendErrorsAsync(cancellation: cancellationToken);
}
