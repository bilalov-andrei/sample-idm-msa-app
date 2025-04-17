using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using IDM.EmployeeService.Domain.Events;

namespace IDM.EmployeeService.UnitTests.Domain
{
    public class EmployeeAggregateTest
    {
        [Fact]
        public void CreateEmployee_ShouldReturnsEmployeeInStatusWork()
        {
            //Arrange
            var fullName = new FullNameBuilder().Build();
            var position = new PositionBuilder().Build();
            var email = new EmailBuilder().Build();

            //Act 
            var employee = new Employee(fullName, position, email);

            //Assert
            Assert.Equal(EmployeeStatus.Work, employee.Status);
        }

        [Fact]
        public void DismissEmployee_houldReturnsEmployeeInStatusDismissed()
        {
            //Arrange
            var fullName = new FullNameBuilder().Build();
            var position = new PositionBuilder().Build();
            var email = new EmailBuilder().Build();

            //Act 
            var employee = new Employee(fullName, position, email);
            employee.Dismiss();

            //Assert
            Assert.Equal(EmployeeStatus.Dismissed, employee.Status);
        }

        [Fact]
        public void CreateEmployee_ShouldRaiseEmployeehiredEvent()
        {
            //Arrange
            var fullName = new FullNameBuilder().Build();
            var position = new PositionBuilder().Build();
            var email = new EmailBuilder().Build();

            //Act 
            var employee = new Employee(fullName, position, email);

            //Assert
            Assert.True(employee.DomainEvents.Any(x => x is EmployeeHiredDomainEvent));
        }


        [Fact]
        public void DismissEmployee_ShouldRaiseEmployeeDismissedEvent()
        {
            //Arrange
            var fullName = new FullNameBuilder().Build();
            var position = new PositionBuilder().Build();
            var email = new EmailBuilder().Build();

            //Act 
            var employee = new Employee(fullName, position, email);
            employee.Dismiss();

            //Assert
            Assert.True(employee.DomainEvents.Any(x => x is EmployeeDismissedDomainEvent));
        }
    }
}
