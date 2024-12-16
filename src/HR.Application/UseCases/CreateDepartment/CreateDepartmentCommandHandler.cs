using AutoMapper;
using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Application.Contracts;
using HR.Domain.Aggregates.Departments;

namespace HR.Application.UseCases.CreateDepartment;

public class CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository, IMapper mapper) : ICommandHandler<CreateDepartmentCommandHandler.Command, Result<DepartmentResponse>>
{
  public record Command(string Name) : ICommand<Result<DepartmentResponse>>;

  public async Task<Result<DepartmentResponse>> Handle(Command request, CancellationToken cancellationToken) =>
    await CreateDepartment(request.Name)
      .ToResult("Department creation failed")
      .Check(departmentRepository.SaveAsync)
      .Map(department => mapper.Map<DepartmentResponse>(department.State));

  private static Maybe<Department> CreateDepartment(string name) 
    => new Department(name);
}
