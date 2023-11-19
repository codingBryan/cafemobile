namespace CafeMobile_parent.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public double price_CP { get; set; }
        public string? image { get; set; }
        public string? description { get; set; }
    }
}
