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
    Description(x => x.WithTags("Employees"));
    AllowAnonymous();
  }
  
  public override async Task HandleAsync(CreateEmployeeRequest request, CancellationToken cancellationToken)
  {
    var command = mapper.Map<CreateEmployeeCommandHandler.Command>(request);
    var result = await mediator.Send(command, cancellationToken);
    if (result.IsSuccess)
      await SendAsync(result.Value, cancellation: cancellationToken);
    else
      await SendErrorsAsync(cancellation: cancellationToken);
  }
}
