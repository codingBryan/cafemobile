namespace CafeMobile_parent.Models.DTO
{
    public class StudentInfo
    {
        public int StudentId { get; set; }
        public string adm_no { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string? image_url { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public double cp_balance { get; set; }
    }
}
