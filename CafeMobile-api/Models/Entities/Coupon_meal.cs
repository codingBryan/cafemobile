namespace CafeMobile_api.Models.Entities
{
    public class Coupon_meal
    {
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
