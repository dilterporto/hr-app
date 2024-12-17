using FastEndpoints;
using HR.Application.UseCases.ChangeEmployeeDepartment;
using HR.Employee.Api.Apis.Employees.Messages;
using HR.Employee.Api.Apis.Employees.Validation;
using MediatR;
using IMapper = AutoMapper.IMapper;

namespace HR.Employee.Api.Apis.Employees;

public class PostEmployeeDepartmentChangeEndpoint(IMediator mediator, IMapper mapper) : Endpoint<ChangeEmployeeDepartmentRequest>
{
  public override void Configure()
  {
    Post("api/employees/{EmployeeId}/departments");
    Description(x => x.WithTags("Employees"));
    Validator<ChangeEmployeeDepartmentValidator>();
    AllowAnonymous();
  }
  
  public override async Task HandleAsync(ChangeEmployeeDepartmentRequest request, CancellationToken cancellationToken)
  {
    var command = mapper.Map<ChangeEmployeeDepartmentCommandHandler.Command>(request);
    var result = await mediator.Send(command, cancellationToken);
    if (result.IsSuccess)
      await SendAsync(result.Value, cancellation: cancellationToken);
    else
      await SendErrorsAsync(cancellation: cancellationToken);
  }
}
