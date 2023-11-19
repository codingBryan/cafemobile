namespace CafeMobile_api.Models.Entities
{
    public class Redemption_meal
    {
        public Guid RedemptionId { get; set; }
        public Redemption Redemption { get; set; }
        public int MealId { get; set; }
        public Meal Meal { get; set; }
    }
}
