namespace IDM.EmployeeService.IntegrationEvents.Events;

public class EmployeeDismissedIntegrationEvent(int employeeId) : IIntegrationEvent
{
    public int EmployeeId { get; } = employeeId;

    public EmployeeEventType EmployeeEventType { get; } = EmployeeEventType.Dismissal;
}
