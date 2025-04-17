using IDM.Common.Application.Commands;
using IDM.Common.Domain;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;

namespace IDM.EmployeeService.Application.Commands.CreateEmployee
{
    public class CreateEmployeeCommandHandler : ICommandHandler<CreateEmployeeCommand, int>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEmployeeCommandHandler(
            IEmployeeRepository repository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee(
                FullName.Create(request.FirstName, request.LastName, request.MiddleName),
                Position.Create(request.Position),
                Email.Create(request.Email));

            var employeeGetByEmail = await _employeeRepository.GetByEmailAsync(request.Email, cancellationToken);
            if (employeeGetByEmail is not null)
                throw new InvalidOperationException($"Email {request.Email} already taken");

            await _unitOfWork.StartTransaction(cancellationToken);

            var employeeId = await _employeeRepository.CreateAsync(employee, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return employeeId;
        }
    }
}
