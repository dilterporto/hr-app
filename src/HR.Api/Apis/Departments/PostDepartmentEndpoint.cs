using FastEndpoints;
using HR.Application.UseCases.CreateDepartment;
using HR.Employee.Api.Apis.Departments.Messages;
using HR.Employee.Api.Apis.Departments.Validation;
using HR.Employee.Api.Apis.Employees.Validation;
using MediatR;

namespace HR.Employee.Api.Apis.Departments;

public class PostDepartmentEndpoint(IMediator mediator) : Endpoint<CreateDepartmentRequest>
{
  public override void Configure()
  {
    Post("api/departments");
    Description(x => x.WithTags("Departments"));
    Validator<CreateDepartmentValidator>();
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateDepartmentRequest request, CancellationToken cancellationToken)
  {
    var command = new CreateDepartmentCommandHandler.Command(request.Name!);
    var result = await mediator.Send(command, cancellationToken);
    if (result.IsSuccess)
      await SendAsync(result.Value, cancellation: cancellationToken);
    else
      await SendErrorsAsync(cancellation: cancellationToken);
  }
}
