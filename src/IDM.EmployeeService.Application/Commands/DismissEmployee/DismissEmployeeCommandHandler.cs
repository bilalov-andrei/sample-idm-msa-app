using IDM.Common.Application.Commands;
using IDM.Common.Domain;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using MediatR;

namespace IDM.EmployeeService.Application.Handlers.DismissEmployee
{
    public class DismissEmployeeCommandHandler : ICommandHandler<DismissEmployeeCommand, Unit>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DismissEmployeeCommandHandler(
            IEmployeeRepository repository,
            IUnitOfWork unitOfWork)
        {
            _employeeRepository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DismissEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Увольняем сотрудника
            var employee = await _employeeRepository.GetByIdAsync(request.Id, cancellationToken);
            if (employee is null)
                throw new InvalidOperationException($"Employee with id {request.Id} not found.");

            await _unitOfWork.StartTransaction(cancellationToken);

            employee.Dismiss();

            await _employeeRepository.UpdateAsync(employee, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
