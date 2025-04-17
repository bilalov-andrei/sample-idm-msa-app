using IDM.Common.Domain;

namespace IDM.AccessManagement.Domain.UserAccountAggregate
{
    public class Right : ValueObject
    {
        public string Value { get; }

        private Right(string name)
            => Value = name;

        public static Right Create(string right)
        {
            return new Right(right);
        }

        public override string ToString()
            => Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
