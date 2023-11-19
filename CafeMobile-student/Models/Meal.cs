namespace CafeMobile.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double price_CP { get; set; }
        public string? image_url { get; set; }
        public string? description { get; set; }
        public bool isSelected { get; set; } = false;
    }
}
