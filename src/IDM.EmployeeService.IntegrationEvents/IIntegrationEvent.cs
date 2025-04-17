using IDM.EmployeeService.IntegrationEvents.Events;

namespace IDM.EmployeeService.IntegrationEvents;

public interface IIntegrationEvent
{
    EmployeeEventType EmployeeEventType { get; }
}
