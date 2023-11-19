using CafeMobile_api.Models.Entities;

namespace CafeMobile_api.DTO
{
    public class Stu_signupDTO
    {
        public string adm_no { get; set; }
        public string first_name { get; set; }
        public string? profile_pic { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string email { get; set; }
        public string phone_number { get; set; }
        public string password { get; set; }
    }
}
