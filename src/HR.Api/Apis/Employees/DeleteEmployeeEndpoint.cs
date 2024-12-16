using FastEndpoints;
using HR.Application.UseCases.DeleteEmployee;
using HR.Employee.Api.Apis.Employees.Messages;
using MediatR;

namespace HR.Employee.Api.Apis.Employees;

public class DeleteEmployeeEndpoint(IMediator mediator) : Endpoint<DeleteEmployeeRequest>
{
  public override void Configure()
  {
    Delete("api/employees/{EmployeeId}");
    Description(x => x.WithTags("Employees"));
    AllowAnonymous();
  }

  public override async Task HandleAsync(DeleteEmployeeRequest request, CancellationToken cancellationToken)
  {
    var result = await mediator.Send(new DeleteEmployeeCommandHandler.Command(request.EmployeeId), cancellationToken);
    if (result.IsFailure)
      await SendErrorsAsync(cancellation: cancellationToken);
    else
    {
      await SendNoContentAsync(cancellation: cancellationToken);
    }
  }
}
