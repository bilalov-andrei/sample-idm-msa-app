
using IDM.Common.Domain;

namespace IDM.EmployeeService.Domain.AggregatesModel.Employee
{
    public class Position : ValueObject
    {
        public string Value { get; }

        private Position(string positionName)
            => Value = positionName;

        public static Position Create(string positionName)
        {
            return new Position(positionName);
        }

        public override string ToString()
            => Value;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
