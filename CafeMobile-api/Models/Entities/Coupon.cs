using CafeMobile_api.DTO;

namespace CafeMobile_api.Models.Entities
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string name { get; set; }
        public string? image { get; set; }
        public double price { get; set; }
        public IEnumerable<Coupon_meal>? couponMeals { get; set; }
        public IEnumerable<StudentCoupon>? purchasedCoupons { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public bool is_active { get; set; } = true;
    }
}
