using System.Text.Json.Serialization;

namespace CafeMobile_api.Models.System.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum StaffRole
    {
        Supervisor,
        Chef,
        Admin

    }
}
