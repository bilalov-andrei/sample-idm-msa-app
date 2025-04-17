using AutoMapper;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using MediatR;

namespace IDM.EmployeeService.Application.Queries.GetAllEmployees
{
    public class GetAllEmployeesQueryHandler : IRequestHandler<GetAllEmployeesQuery, GetAllEmployeesQueryResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetAllEmployeesQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<GetAllEmployeesQueryResponse> Handle(GetAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetAllAsync(cancellationToken);

            return new GetAllEmployeesQueryResponse()
            {
                Items = _mapper.Map<List<EmployeeDto>>(employee)
            };
        }
    }
}
