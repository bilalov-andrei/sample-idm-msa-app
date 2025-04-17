namespace IDM.Common.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        ValueTask StartTransaction(CancellationToken cancellationToken);
        Task SaveChangesAsync(CancellationToken cancellationToken);
    }
}
