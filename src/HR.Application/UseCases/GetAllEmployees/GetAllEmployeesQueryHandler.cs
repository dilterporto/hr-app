using AutoMapper;
using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Abstractions.EventSourcing;
using HR.Application.Contracts;
using HR.Persistence.Reading.Projections;

namespace HR.Application.UseCases.GetAllEmployees;

public class GetAllEmployeesQueryHandler(IProjectionsReader projectionsReader, IMapper mapper) : IQueryHandler<GetAllEmployeesQueryHandler.Query, Result<IEnumerable<EmployeeResponse>>>
{
  public record Query : IQuery<Result<IEnumerable<EmployeeResponse>>>;
  

  public async Task<Result<IEnumerable<EmployeeResponse>>> Handle(Query request, CancellationToken cancellationToken)
  {
    var employeeList = await projectionsReader
      .GetAllAsync<EmployeeProjection>();
    
    return employeeList.AsEnumerable()
      .Select(mapper.Map<EmployeeResponse>)
      .ToList();
  }
}
