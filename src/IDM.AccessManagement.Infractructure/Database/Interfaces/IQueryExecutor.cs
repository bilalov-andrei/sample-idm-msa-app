using IDM.Common.Domain;

namespace IDM.AccessManagement.Infractructure.Database.Interfaces
{
    public interface IQueryExecutor
    {
        Task<T> Execute<T>(T entity, Func<Task> method) where T : IEntity;

        Task<T> Execute<T>(Func<Task<T>> method) where T : IEntity;

        Task<IEnumerable<T>> Execute<T>(Func<Task<IEnumerable<T>>> method) where T : IEntity;
    }
}
