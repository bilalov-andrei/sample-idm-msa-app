using IDM.Common.Domain;
using IDM.EmployeeService.IntegrationEvents;

namespace IDM.EmployeeService.Infractructure.Processing.Outbox
{
    public interface IEventMapper
    {
        IIntegrationEvent Map(IDomainEvent @event);
        IEnumerable<IIntegrationEvent> MapAll(IEnumerable<IDomainEvent> events);
    }
}
