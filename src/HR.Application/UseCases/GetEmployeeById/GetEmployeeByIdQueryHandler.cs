using AutoMapper;
using CSharpFunctionalExtensions;
using HR.Abstractions.CQRS;
using HR.Abstractions.EventSourcing;
using HR.Application.Contracts;
using HR.Persistence.Reading.Projections;

namespace HR.Application.UseCases.GetEmployeeById;

public class GetEmployeeByIdQueryHandler(IProjectionsReader projectionsReader, IMapper mapper) 
  : IQueryHandler<GetEmployeeByIdQuery, Result<EmployeeResponse>>
{
  public async Task<Result<EmployeeResponse>> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken) 
    => await projectionsReader
      .GetByIdAsync<EmployeeProjection>(request.Id)
      .ToResult("Employee not found")
      .Map(mapper.Map<EmployeeResponse>);
}
