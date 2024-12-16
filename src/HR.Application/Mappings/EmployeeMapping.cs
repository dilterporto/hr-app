using AutoMapper;
using HR.Application.Contracts;
using HR.Application.UseCases.CreateEmployee;
using HR.Domain.Aggregates.Employees;

namespace HR.Application.Mappings;

public class EmployeeMapping : Profile
{
  public EmployeeMapping()
  {
    CreateMap<CreateEmployeeCommandHandler.Command, EmployeeState>();
    CreateMap<EmployeeState, EmployeeResponse>();
  }
}
