using IDM.Common.Domain;
using IDM.EmployeeService.Domain.Events;
using IDM.EmployeeService.IntegrationEvents;
using IDM.EmployeeService.IntegrationEvents.Events;

namespace IDM.EmployeeService.Infractructure.Processing.Outbox
{
    public class EventMapper : IEventMapper
    {
        public IEnumerable<IIntegrationEvent> MapAll(IEnumerable<IDomainEvent> events)
                => events.Select(Map);

        public IIntegrationEvent Map(IDomainEvent @event) =>
            @event switch
            {
                EmployeeHiredDomainEvent domainEvent =>
                    new EmployeeHiredIntegrationEvent(
                        employeeId: domainEvent.Employee.Id, domainEvent.Employee.FullName.ToString(), domainEvent.Employee.Position.Value),
                EmployeeDismissedDomainEvent domainEvent =>
                    new EmployeeDismissedIntegrationEvent(
                        employeeId: domainEvent.Employee.Id),
                _ => throw new Exception($"Cannot map domain event {nameof(@event)} into integration.")
            };
    }
}
