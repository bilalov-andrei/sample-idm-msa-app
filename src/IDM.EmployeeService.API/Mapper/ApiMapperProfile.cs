using AutoMapper;
using IDM.EmployeeService.API.Employees;
using IDM.EmployeeService.API.InputModels;
using IDM.EmployeeService.Application.Commands.CreateEmployee;
using IDM.EmployeeService.Application.Queries.GetAllEmployees;

namespace IDM.EmployeeService.API.Mapper
{
    public class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            CreateMap<CreateEmployeeViewModel, CreateEmployeeCommand>(MemberList.Destination);

            CreateMap<EmployeeDto, EmployeeViewModel>(MemberList.Destination);
        }
    }
}
