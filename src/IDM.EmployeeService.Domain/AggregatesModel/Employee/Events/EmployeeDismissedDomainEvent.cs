using IDM.Common.Domain;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;

namespace IDM.EmployeeService.Domain.Events;

public class EmployeeDismissedDomainEvent : IDomainEvent
{
    public Employee Employee { get; }


    public EmployeeDismissedDomainEvent(Employee employee)
    {
        Employee = employee;
    }
}

