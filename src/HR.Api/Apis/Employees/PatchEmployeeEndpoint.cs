using CSharpFunctionalExtensions;
using FastEndpoints;
using HR.Application.Contracts;
using HR.Application.UseCases.ChangeEmployee;
using HR.Employee.Api.Apis.Employees.Messages;
using MediatR;
using IMapper = AutoMapper.IMapper;

namespace HR.Employee.Api.Apis.Employees;

public class PatchEmployeeEndpoint(IMediator mediator, IMapper mapper) : Endpoint<ChangeEmployeeRequest, EmployeeResponse>
{
  public override void Configure()
  {
    Patch("api/employees/{EmployeeId}");
    AllowAnonymous();
  }

  public override async Task HandleAsync(ChangeEmployeeRequest request, CancellationToken cancellationToken)
  {
    var command = mapper.Map<ChangeEmployeeCommandHandler.Command>(request);
    var result = await mediator.Send(command, cancellationToken);
    await HandleResult(result, cancellationToken);
  }
  
  private Task HandleResult(Result<EmployeeResponse> result, CancellationToken cancellationToken) =>
    result.IsSuccess ? SendAsync(result.Value, cancellation: cancellationToken) : SendErrorsAsync(cancellation: cancellationToken);
}
