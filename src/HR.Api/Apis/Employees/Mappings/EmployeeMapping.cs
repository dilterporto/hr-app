using AutoMapper;
using HR.Application.Contracts;
using HR.Application.UseCases.ChangeEmployee;
using HR.Application.UseCases.CreateEmployee;
using HR.Domain.Shared.ValueObjects;
using HR.Employee.Api.Apis.Employees.Messages;
using HR.Persistence.Reading.Projections;

namespace HR.Employee.Api.Apis.Employees.Mappings;

public class EmployeeMapping : Profile
{
  public EmployeeMapping()
  {
    CreateMap<CreateEmployeeRequest, CreateEmployeeCommandHandler.Command>();
    CreateMap<ChangeEmployeeRequest, ChangeEmployeeCommandHandler.Command>();
    CreateMap<EmployeeProjection, EmployeeResponse>()
      .ForMember(x => x.Address, x => x.MapFrom(y => new Address()
      {
        Line1         = y.AddressLine1,
        Line2         = y.AddressLine2,
        City          = y.AddressCity,
        State         = y.AddressState,
        ZipCode       = y.AddressZipCode,
      }));
  }
}
