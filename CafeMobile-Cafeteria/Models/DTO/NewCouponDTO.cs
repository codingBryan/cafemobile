using Cafemobile_Cafeteria.Models.System;

namespace Cafemobile_Cafeteria.Models.DTO
{
    public class NewCouponDTO
    {
        public int CouponId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string? image { get; set; }
        public List<int> meal_Ids { get; set; }
    }
}
