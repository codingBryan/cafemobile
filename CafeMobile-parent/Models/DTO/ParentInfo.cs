namespace CafeMobile_parent.Models.DTO
{
    public class ParentInfo
    {
        public int ParentId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string phone_number { get; set; }
        public string email { get; set; }
    }
}
