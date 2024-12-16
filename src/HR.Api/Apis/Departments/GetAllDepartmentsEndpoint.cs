using FastEndpoints;
using HR.Application.Contracts;
using HR.Application.UseCases.GetAllDepartments;
using MediatR;

namespace HR.Employee.Api.Apis.Departments;

public class GetAllDepartmentsEndpoint(IMediator mediator) : EndpointWithoutRequest<IEnumerable<DepartmentResponse>>
{
  public override void Configure()
  {
    Get("api/departments");
    Description(x => x.WithTags("Departments"));
    AllowAnonymous();  
  }

  public override async Task HandleAsync(CancellationToken cancellationToken)
  {
    var query = new GetAllDepartmentsQueryHandler.Query();
    var result = await mediator.Send(query, cancellationToken);
    if (result.IsSuccess)
      await SendAsync(result.Value, cancellation: cancellationToken);
    else
      await SendErrorsAsync(cancellation: cancellationToken);
  }
}
