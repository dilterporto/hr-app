using CSharpFunctionalExtensions;
using FastEndpoints;
using HR.Application.Contracts;
using HR.Application.UseCases.CreateEmployee;
using HR.Employee.Api.Apis.Employees.Messages;
using MediatR;
using IMapper = AutoMapper.IMapper;

namespace HR.Employee.Api.Apis.Employees;

public class PostEmployeeEndpoint(IMediator mediator, IMapper mapper) : Endpoint<CreateEmployeeRequest, EmployeeResponse>
{
  public override void Configure()
  {
    Post("api/employees");
    AllowAnonymous();
  }
  
  public override async Task HandleAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
  {
    var command = mapper.Map<CreateEmployeeCommandHandler.Command>(request);
    
    var result = await mediator.Send(command, cancellationToken);

    await HandleResult(result, cancellationToken);
  }
  
  private Task HandleResult(Result<EmployeeResponse> result, CancellationToken cancellationToken) =>
    result.IsSuccess ? SendAsync(result.Value, cancellation: cancellationToken) : SendErrorsAsync(cancellation: cancellationToken);
}
