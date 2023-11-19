

namespace CafeMobile_parent.Models
{
    public class Redemption
    {
        public int RedemptionId { get; set; }
        public string meal_name { get; set; }
        public string image_url { get; set; }
        public int price { get; set; }
        public DateOnly created_at { get; set; }
    }
}
