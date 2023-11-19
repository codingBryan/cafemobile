using Cafemobile_Cafeteria.Models.System;

namespace Cafemobile_Cafeteria.Models.DTO
{
    public class NewCouponDTO
    {
        public string name { get; set; }
        public double price { get; set; }
        public byte[]? image { get; set; }
        public List<int> meal_Ids { get; set; }
    }
}
