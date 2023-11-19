using CafeMobile_api.Models.Entities;

namespace CafeMobile_api.DTO
{
    public class StudentSale
    {
        public string meal_name { get; set; }
        public int units { get; set; }
        public double price_cp { get; set; }
        public string created_at { get; set; }
    }
}
