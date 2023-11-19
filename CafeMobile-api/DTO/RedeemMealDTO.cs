using CafeMobile_api.Models.Entities;

namespace CafeMobile_api.DTO
{
    public class RedeemMealDTO
    {
        public int MealId { get; set; }
        public int quantity { get; set; }
        public double price { get; set; }
        public double price_CP { get; set; }
        
    }
}
