using AutoMapper;
using IDM.EmployeeService.Application.Commands.CreateEmployee;
using IDM.EmployeeService.Application.Queries.GetAllEmployees;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;

namespace IDM.EmployeeService.Application.Mapper
{
    public class ApplicationServicesMapperProfile : Profile
    {
        public ApplicationServicesMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>(MemberList.Destination)
                .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FullName.FirstName))
                .ForMember(d => d.LastName, o => o.MapFrom(s => s.FullName.LastName))
                .ForMember(d => d.MiddleName, o => o.MapFrom(s => s.FullName.MiddleName))
                .ForMember(d => d.Position, o => o.MapFrom(s => s.Position.Value))
                .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Value));

            CreateMap<CreateEmployeeCommand, Employee>(MemberList.Destination)
                .ForMember(d => d.Id, o => o.MapFrom(s => 0));
        }
    }
}
