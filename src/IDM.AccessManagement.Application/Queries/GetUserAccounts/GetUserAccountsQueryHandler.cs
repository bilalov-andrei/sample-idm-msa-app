using AutoMapper;
using IDM.AccessManagement.Domain.UserAccountAggregate;
using MediatR;

namespace IDM.AccessManagement.Application.Queries.GetUserAccounts
{
    public sealed record GetUserAccountsQuery(int? employeeID, int? systemID) : IRequest<GetUserAccountsQueryResponse>;

    public class GetUserAccountsQueryHandler : IRequestHandler<GetUserAccountsQuery, GetUserAccountsQueryResponse>
    {
        private readonly IUserAccountRepository _userAccountRepository;
        private readonly IMapper _mapper;

        public GetUserAccountsQueryHandler(IUserAccountRepository userAccountRepository, IMapper mapper)
        {
            _userAccountRepository = userAccountRepository;
            _mapper = mapper;
        }
        public async Task<GetUserAccountsQueryResponse> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
        {
            var userAccounts = await _userAccountRepository.GetAll(request.employeeID, request.systemID, cancellationToken);

            return new GetUserAccountsQueryResponse()
            {
                Items = _mapper.Map<List<UserAccountDto>>(userAccounts)
            };
        }
    }
}
