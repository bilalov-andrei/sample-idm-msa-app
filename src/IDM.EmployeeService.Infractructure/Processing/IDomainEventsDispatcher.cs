namespace IDM.EmployeeService.Infrastructure.Processing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync(CancellationToken cancellationToken);
    }
}
