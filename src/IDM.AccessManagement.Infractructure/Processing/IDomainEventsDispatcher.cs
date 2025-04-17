namespace IDM.AccessManagement.Infrastructure.Processing
{
    public interface IDomainEventsDispatcher
    {
        Task DispatchEventsAsync(CancellationToken cancellationToken);
    }
}
