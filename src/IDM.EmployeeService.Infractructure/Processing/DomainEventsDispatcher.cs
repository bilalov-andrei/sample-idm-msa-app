using IDM.EmployeeService.Infractructure.Database.Interfaces;
using IDM.EmployeeService.Infrastructure.Processing;
using IDM.EmployeeService.IntegrationEvents;
using MediatR;
using Newtonsoft.Json;

namespace IDM.EmployeeService.Infractructure.Processing.Outbox
{
    public class DomainEventsDispatcher : IDomainEventsDispatcher
    {
        private readonly IMediator _mediator;
        private readonly IChangeTracker _changeTracker;
        private readonly IOutboxMessageRepository _outboxMessageRepository;
        private readonly IEventMapper _eventMapper;

        public DomainEventsDispatcher(
            IMediator mediator,
            IChangeTracker changeTracker,
            IOutboxMessageRepository outboxMessageRepository,
            IEventMapper eventMapper)
        {
            _mediator = mediator;
            _changeTracker = changeTracker;
            _outboxMessageRepository = outboxMessageRepository;
            _eventMapper = eventMapper;
        }

        public async Task DispatchEventsAsync(CancellationToken cancellationToken)
        {
            var trackedEntities =
                  _changeTracker.TrackedEntities
                  .ToDictionary(x => x.Key, x =>
                  {
                      var events = x.Value.DomainEvents.ToList();
                      x.Value.ClearDomainEvents();
                      return events;
                  });

            var tasks = trackedEntities.SelectMany(x => x.Value).Select(async (domainEvent) =>
            {
                await _mediator.Publish(domainEvent);
            });

            await Task.WhenAll(tasks);

            foreach (var trackedEntity in trackedEntities)
            {
                foreach (IIntegrationEvent integrationEvent in _eventMapper.MapAll(trackedEntity.Value))
                {
                    var outboxMessage = new OutboxMessage()
                    {
                        Key = trackedEntity.Key.ToString(), //use a aggregate’s ID as the partition key, ensuring that events are routed to the same partition and processed in the correct sequence.
                        EventType = integrationEvent.EmployeeEventType,
                        Payload = JsonConvert.SerializeObject(integrationEvent)
                    };
                    await _outboxMessageRepository.CreateAsync(outboxMessage, cancellationToken);
                }
            }

        }
    }
}
