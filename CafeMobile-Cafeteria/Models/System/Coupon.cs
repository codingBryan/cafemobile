using System.Collections.ObjectModel;

namespace Cafemobile_Cafeteria.Models.System
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public byte[]? image { get; set; }
        public bool is_active { get; set; } = true;
        public ImageSource? dsplayImage { get; set; }
        public IEnumerable<Meal>? meals { get; set; } = new List<Meal>();
    }
}
