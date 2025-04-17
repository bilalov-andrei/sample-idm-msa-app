using IDM.AccessManagement.Domain.DomainServices;
using IDM.AccessManagement.Domain.DomainServices.Models;
using IDM.AccessManagement.Domain.UserAccountAggregate;
using IDM.Common.Application.Commands;
using IDM.Common.Domain;
using MediatR;

namespace IDM.AccessManagement.Application.Commands.SetupAccess
{
    public class SetupAccessCommandHandler : ICommandHandler<SetupAccessCommand, Unit>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetupAccessCommandHandler(
            IUserAccountRepository userAccountRepository,
            IUnitOfWork unitOfWork)
        {
            _userAccountRepository = userAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(SetupAccessCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.StartTransaction(cancellationToken);

            var remoteAccessManaging = new RemoteAccessManaging();
            await remoteAccessManaging.SetUpAccess(_userAccountRepository, new EmployeeWorkInfo()
            {
                Id = request.EmployeeId,
                Name = request.EmployeeName,
                Position = request.EmployeePosition,
            }, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
