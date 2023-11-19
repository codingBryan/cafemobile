namespace Cafemobile_Cafeteria.Models.DTO
{
    public class MealDisplay
    {
        public int MealId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public double price_CP { get; set; }
        public byte[]? image { get; set; }
        public string? description { get; set; }
        public bool isSelected { get; set; } = false;
        public ImageSource? displayImage { get; set; }
    }
}
