using MediatR;

namespace IDM.Common.Domain
{
    public interface IDomainEvent : INotification
    {

    }

    public interface IEntity
    {
        IReadOnlyCollection<IDomainEvent> DomainEvents { get; }

        void AddDomainEvent(IDomainEvent eventItem);

        void RemoveDomainEvent(IDomainEvent eventItem);

        void ClearDomainEvents();

    }

    public abstract class Entity<TKey> : IEntity where TKey : IEquatable<TKey>
    {
        private int? _requestedHashCode;

        public virtual TKey Id { get; protected set; }

        private readonly List<IDomainEvent> _domainEvents = new();

        public IReadOnlyCollection<IDomainEvent> DomainEvents
            => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent eventItem)
            => _domainEvents.Add(eventItem);

        public void RemoveDomainEvent(IDomainEvent eventItem)
            => _domainEvents.Remove(eventItem);

        public void ClearDomainEvents()
            => _domainEvents.Clear();

        public bool IsTransient()
            => Id.Equals(default);

        public override bool Equals(object obj)
        {
            if (obj is not Entity<TKey> entity)
                return false;

            if (ReferenceEquals(this, entity))
                return true;

            if (GetType() != entity.GetType())
                return false;

            if (entity.IsTransient() || IsTransient())
                return false;
            else
                return entity.Id.Equals(Id);
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                _requestedHashCode ??= HashCode.Combine(Id, 31);

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();

        }
        public static bool operator ==(Entity<TKey> left, Entity<TKey> right)
        {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(Entity<TKey> left, Entity<TKey> right)
        {
            return !(left == right);
        }
    }
}
