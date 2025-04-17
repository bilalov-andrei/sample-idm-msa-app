using IDM.AccessManagement.Domain.UserAccountAggregate;
using IDM.Common.Application.Commands;
using IDM.Common.Domain;
using MediatR;

namespace IDM.AccessManagement.Application.Commands.RevokeAccess
{
    public class RevokeAccessCommandHandler : ICommandHandler<RevokeAccessCommand, Unit>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RevokeAccessCommandHandler(
            IUserAccountRepository userAccountRepository,
            IUnitOfWork unitOfWork)
        {
            _userAccountRepository = userAccountRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(RevokeAccessCommand request, CancellationToken cancellationToken)
        {
            var userAccounts = await _userAccountRepository.GetAll(request.EmployeeId, null, cancellationToken);

            await _unitOfWork.StartTransaction(cancellationToken);

            var tasks = userAccounts.Select(async x =>
            {
                x.Revoke();
                await _userAccountRepository.UpdateAsync(x, cancellationToken);
            });

            await Task.WhenAll(tasks);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
