using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Domain.Aggregates.Departments;

namespace HR.Application.UseCases.CreateDepartment;

public class CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository) : ICommandHandler<CreateDepartmentCommandHandler.Command, Result>
{
  public record Command(string Name) : ICommand<Result>;

  public async Task<Result> Handle(Command request, CancellationToken cancellationToken) =>
    await CreateDepartment(request.Name)
      .ToResult("Department creation failed")
      .Check(departmentRepository.SaveAsync);

  private static Maybe<Department> CreateDepartment(string name) 
    => new Department(name);
}
