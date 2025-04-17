using System.Text.Json.Serialization;

namespace IDM.AccessManagement.Domain.UserAccountAggregate
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserAccountStatus
    {
        Active = 1,
        Revoked = 2
    }
}
