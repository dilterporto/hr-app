using AutoMapper;
using HR.Application.Contracts;
using HR.Domain.Aggregates.Employees;

namespace HR.Application.UseCases.CreateEmployee.Mappings;

public class EmployeeMapping : Profile
{
  public EmployeeMapping()
  {
    CreateMap<CreateEmployeeCommandHandler.Command, EmployeeState>();
    CreateMap<EmployeeState, EmployeeResponse>();
  }
}
