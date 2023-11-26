namespace CafeMobile_api.DTO
{
    public class StudentPurchase
    {
        public int mealId { get; set; }
        public GetMealDTO meal { get; set; }
        public string? image { get; set; }
        public int units { get; set; }
        public double price { get; set; }
        public double price_cp => price / 10;
        public string created_at { get; set; }
    }
}
