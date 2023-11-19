namespace CafeMobile_parent.Models.DTO
{
    public class StudentPurchases
    {
        public int mealId { get; set; }
        public string meal_name { get; set; }
        public int units { get; set; }
        public string? image { get; set; }
        public double price { get; set; }
        public double price_cp { get; set; }
        public string created_at { get; set; }
    }
}
