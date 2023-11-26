using System.Collections.ObjectModel;

namespace CafeMobile.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string? image { get; set; }
        public bool is_active { get; set; } = true;
        public ObservableCollection<Meal>? meals { get; set; }
    }
}
