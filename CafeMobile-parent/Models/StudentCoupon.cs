

namespace CafeMobile_parent.Models
{
    public class StudentCoupon
    {
        public int CouponId { get; set; }
        public string? image { get; set; }
        public string name { get; set; }
        public double balance { get; set; }
        public bool is_active { get; set; }
    }
}
