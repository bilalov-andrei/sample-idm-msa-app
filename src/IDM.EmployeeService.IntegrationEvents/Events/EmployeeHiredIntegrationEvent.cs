namespace IDM.EmployeeService.IntegrationEvents.Events;

public class EmployeeHiredIntegrationEvent(int employeeId, string employeeName, string employeePosition) : IIntegrationEvent
{
    public int EmployeeId { get; } = employeeId;
    public string EmployeeName { get; } = employeeName;
    public string EmployeePosition { get; } = employeePosition;

    public EmployeeEventType EmployeeEventType { get; } = EmployeeEventType.Hiring;
}
