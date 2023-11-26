
namespace CafeMobile.Models
{
    public class CartItem
    {
        public int MealId { get; set; }
        public string image { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double price_CP { get; set; }
        public int quantity { get; set; }
        public double bill => price_CP*quantity;
    }
}
