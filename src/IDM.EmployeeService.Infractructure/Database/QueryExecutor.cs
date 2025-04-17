using IDM.Common.Domain;
using IDM.EmployeeService.Infractructure.Database.Interfaces;

namespace IDM.EmployeeService.Infractructure.Database
{
    public class QueryExecutor : IQueryExecutor
    {
        private readonly IChangeTracker _changeTracker;

        public QueryExecutor(IChangeTracker changeTracker)
        {
            _changeTracker = changeTracker;
        }

        public async Task<T> Execute<T>(T entity, Func<Task> method) where T : IEntity
        {
            await method();
            _changeTracker.Track(entity);
            return entity;
        }

        public async Task<T> Execute<T>(Func<Task<T>> method) where T : IEntity
        {
            var result = await method();
            _changeTracker.Track(result);
            return result;
        }

        public async Task<IEnumerable<T>> Execute<T>(Func<Task<IEnumerable<T>>> method) where T : IEntity
        {
            var result = (await method()).ToList();
            foreach (var entity in result)
            {
                _changeTracker.Track(entity);
            }

            return result;
        }
    }
}
