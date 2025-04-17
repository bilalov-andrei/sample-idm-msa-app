using IDM.EmployeeService.Domain.AggregatesModel.Employee;

namespace IDM.EmployeeService.UnitTests.Domain
{

    public class FullNameBuilder
    {
        public FullName Build()
        {
            return FullName.Create("test first name", "test last name", "test middle name");
        }
    }

    public class PositionBuilder
    {
        public Position Build()
        {
            return Position.Create("test position");
        }
    }

    public class EmailBuilder
    {
        public Email Build()
        {
            return Email.Create("test@test.ru");
        }
    }
}
