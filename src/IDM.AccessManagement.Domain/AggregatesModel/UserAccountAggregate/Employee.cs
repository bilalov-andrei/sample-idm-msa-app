using IDM.Common.Domain;

namespace IDM.AccessManagement.Domain.UserAccountAggregate
{
    public class Employee : ValueObject
    {
        public int Id { get; }
        public string Name { get; }

        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Id;
        }
    }
}
