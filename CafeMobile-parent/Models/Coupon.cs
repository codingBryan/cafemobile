

namespace CafeMobile_parent.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string name { get; set; }
        public string? image_url { get; set; }
        public double price { get; set; }
        public IEnumerable<Meal>? meals { get; set; }
    }
}
