using AutoMapper;
using IDM.EmployeeService.Application.Queries.GetAllEmployees;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using MediatR;

namespace IDM.EmployeeService.Application.Queries.GetEmployeeById
{
    public sealed record GetEmployeeByIdQuery(int Id) : IRequest<EmployeeDto>;

    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee =  await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);

            return employee == null ? null : _mapper.Map<EmployeeDto>(employee);
        }
    }
}
