using CSharpFunctionalExtensions;
using FastEndpoints;
using HR.Application.Contracts;
using HR.Application.UseCases.GetAllEmployees;
using MediatR;

namespace HR.Employee.Api.Apis.Employees;

public class GetAllEmployeesEndpoint(IMediator mediator) : EndpointWithoutRequest<IEnumerable<EmployeeResponse>>
{
  public override void Configure()
  {
    Get("api/employees");
    Description(x => x.WithTags("Employees"));
    AllowAnonymous();  
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var query = new GetAllEmployeesQueryHandler.Query();
    var result = await mediator.Send(query, cancellationToken);
    if (result.IsSuccess)
      await SendAsync(result.Value, cancellation: cancellationToken);
    else
      await SendErrorsAsync(cancellation: cancellationToken);
  }
}
