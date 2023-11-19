using CafeMobile_api.Models.System.Enums;

namespace CafeMobile_api.DTO
{
    public class StaffInfo
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string? profile_pic { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string role { get; set; }
    }
}
