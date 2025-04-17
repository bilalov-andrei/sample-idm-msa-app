using IDM.AccessManagement.Infractructure.Database.Interfaces;
using IDM.Common.Domain;
using System.Collections.Concurrent;

namespace IDM.AccessManagement.Infractructure.Database
{
    public class ChangeTracker : IChangeTracker
    {
        public IDictionary<int, IEntity> TrackedEntities => _usedEntitiesBackingField;

        private readonly ConcurrentDictionary<int, IEntity> _usedEntitiesBackingField;

        public ChangeTracker()
        {
            _usedEntitiesBackingField = new ConcurrentDictionary<int, IEntity>();
        }

        public void Track(IEntity entity)
        {
            _usedEntitiesBackingField.TryAdd(entity.GetHashCode(), entity);
        }
    }
}
