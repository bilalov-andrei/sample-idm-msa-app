using IDM.Common.Domain;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;

namespace IDM.EmployeeService.Domain.Events;

public class EmployeeHiredDomainEvent : IDomainEvent
{
    public Employee Employee { get; }

    public EmployeeHiredDomainEvent(Employee employee)
    {
        Employee = employee;
    }
}

