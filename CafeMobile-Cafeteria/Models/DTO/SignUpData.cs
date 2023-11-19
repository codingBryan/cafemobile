
using Cafemobile_Cafeteria.Models.Enums;

namespace Cafemobile_Cafeteria.Models.DTO
{
    public class SignUpData
    {
        public string first_name { get; set; }
        public string last_name { get; set; } = String.Empty;
        public byte[]? profile_pic { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string role { get; set; } = "Admin";
        public string password { get; set; }
    }
}
