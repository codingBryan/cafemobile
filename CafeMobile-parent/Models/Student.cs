

namespace CafeMobile_parent.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string adm_no { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string? image_url { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public double cp_balance { get; set; }
        public IEnumerable<StudentCoupon>? coupons { get; set; }
        public IEnumerable<Redemption>? redemptions { get; set; }
    }
}
