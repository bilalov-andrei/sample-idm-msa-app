using IDM.Common.Domain;

namespace IDM.EmployeeService.Domain.AggregatesModel.Employee
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string emailString)
            => Value = emailString;

        public static Email Create(string emailString)
        {
            return new Email(emailString);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
