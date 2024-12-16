using AutoMapper;
using HR.Application.Contracts;
using HR.Domain.Aggregates.Departments;
using HR.Persistence.Reading.Projections;

namespace HR.Application.Mappings;

public class DepartmentMapping : Profile
{
  public DepartmentMapping()
  {
    CreateMap<DepartmentProjection, DepartmentResponse>();
    CreateMap<DepartmentState, DepartmentResponse>();
  }
}
