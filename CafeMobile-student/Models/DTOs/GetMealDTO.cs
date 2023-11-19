namespace CafeMobile.Models.DTOs
{
    public class GetMealDTO
    {
        public int MealId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double price_CP { get; set; }
        public byte[]? image { get; set; }
        public string? description { get; set; }
    }
}
