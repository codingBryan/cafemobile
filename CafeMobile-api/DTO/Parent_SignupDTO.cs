namespace CafeMobile_api.DTO
{
    public class Parent_SignupDTO
    {
        public string first_name { get; set; }
        public string? profile_pic { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string phone_number { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }
}
