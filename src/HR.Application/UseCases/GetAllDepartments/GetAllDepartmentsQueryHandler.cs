using AutoMapper;
using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Abstractions.EventSourcing;
using HR.Application.Contracts;
using HR.Persistence.Reading.Projections;

namespace HR.Application.UseCases.GetAllDepartments;

public class GetAllDepartmentsQueryHandler(IProjectionsReader projectionsReader, IMapper mapper) 
  : IQueryHandler<GetAllDepartmentsQueryHandler.Query, Result<IEnumerable<DepartmentResponse>>>
{
  public record Query : IQuery<Result<IEnumerable<DepartmentResponse>>>;

  public async Task<Result<IEnumerable<DepartmentResponse>>> Handle(Query request, CancellationToken cancellationToken) =>
    (await projectionsReader.GetAllAsync<DepartmentProjection>())
    .AsEnumerable()
    .Select(mapper.Map<DepartmentResponse>)
    .ToList();
}
