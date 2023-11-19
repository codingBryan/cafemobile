namespace CafeMobile_api.Models.Entities
{
    public class Parent
    {
        public int ParentId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string? profile_pic { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public string password_hash { get; set; }
        public IEnumerable<Parents_Students>? students { get; set; }
        public IEnumerable<Transaction>? transactions { get; set; }
    }
}
