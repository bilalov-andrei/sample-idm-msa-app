using IDM.AccessManagement.Infractructure.Database.Interfaces;
using IDM.AccessManagement.Infrastructure.Processing;
using MediatR;

namespace IDM.AccessManagement.Infractructure.Processing
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly IChangeTracker _changeTracker;

        public DomainEventsDispatcher(
            IMediator mediator,
            IChangeTracker changeTracker)
        {
            _mediator = mediator;
            _changeTracker = changeTracker;
        }

        public async Task DispatchEventsAsync(CancellationToken cancellationToken)
        {


        }
    }
}
