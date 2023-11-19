namespace CafeMobile_api.Models.Entities
{
    public class Meal
    {
        public int MealId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double? price_CP { get; set; }
        public string category { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
        public IEnumerable<Coupon_meal>? coupons { get; set; }
        public IEnumerable<Redemption_meal>? redemptions { get; set; }

    }
}
