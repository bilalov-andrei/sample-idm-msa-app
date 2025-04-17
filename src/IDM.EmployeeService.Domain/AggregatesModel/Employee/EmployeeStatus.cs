using System.Text.Json.Serialization;

namespace IDM.EmployeeService.Domain.AggregatesModel.Employee
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum EmployeeStatus
    {
        Work = 1,
        Dismissed = 2
    }
}
