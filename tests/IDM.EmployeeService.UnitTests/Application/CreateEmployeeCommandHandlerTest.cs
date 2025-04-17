using IDM.Common.Domain;
using IDM.EmployeeService.Application.Commands.CreateEmployee;
using IDM.EmployeeService.Domain.AggregatesModel.Employee;
using IDM.EmployeeService.UnitTests.Domain;
using NSubstitute;

namespace IDM.EmployeeService.UnitTests.Application;

public class CreateEmployeeCommandHandlerTest
{
    [Fact]
    public async Task CreateEmployee_WithNotUniqueEmail_ThrowsException()
    {
        //Arrange
        var notUniqueEmail = "test@test.ru";
        var fakeEmployee = new Employee(new FullNameBuilder().Build(), new PositionBuilder().Build(), Email.Create(notUniqueEmail));
        var employeeRepositoryMock = Substitute.For<IEmployeeRepository>();
        var unitOfWork = Substitute.For<IUnitOfWork>();
        var fakeCreateEmployeeCommand = new CreateEmployeeCommand("test first name", "test last name", "test middle name", notUniqueEmail, "test position");

        employeeRepositoryMock.GetByEmailAsync(notUniqueEmail, new CancellationToken()).Returns(Task.FromResult(fakeEmployee));

        //Act
        var handler = new CreateEmployeeCommandHandler(employeeRepositoryMock, unitOfWork);
        var act = async () => await handler.Handle(fakeCreateEmployeeCommand, new CancellationToken());

        //Assert
        await Assert.ThrowsAsync<InvalidOperationException>(act);
    }

    protected Employee GetFakeEmployee()
    {
        return new Employee(new FullNameBuilder().Build(), new PositionBuilder().Build(), new EmailBuilder().Build());
    }
}
