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
    AllowAnonymous();  
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var query = new GetAllEmployeesQueryHandler.Query();
    
    var result = await mediator.Send(query, cancellationToken);

    await HandleResult(result, cancellationToken);    
  }
  
  private Task HandleResult(Result<IEnumerable<EmployeeResponse>> result, CancellationToken cancellationToken) =>
    result.IsSuccess ? SendAsync(result.Value, cancellation: cancellationToken) : SendErrorsAsync(cancellation: cancellationToken);
}
