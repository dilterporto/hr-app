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
    Description(x => x.WithTags("Employees"));
    AllowAnonymous();
  }
  
  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var query = new GetEmployeeByIdQuery { Id = Guid.Parse(Route<string>("id")!) };
    var result = await mediator.Send(query, cancellationToken);
    if (result.IsSuccess)
      await SendAsync(result.Value, cancellation: cancellationToken);
    else
      await SendErrorsAsync(cancellation: cancellationToken);
  }
}
