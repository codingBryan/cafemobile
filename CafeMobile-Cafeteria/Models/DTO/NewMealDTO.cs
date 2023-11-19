

namespace Cafemobile_Cafeteria.Models.DTO
{
    public class NewMealDTO
    {
        public string name { get; set; }
        public double price { get; set; }
        public string category { get; set; }
        public byte[]? image { get; set; }
        public string description { get; set; }
    }
}
