namespace CafeMobile_parent.Models.DTO
{
    public class GetCouponDTO
    {
        public int CouponId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double balance { get; set; }
        public string? image { get; set; }
        public bool is_active { get; set; }
        public IEnumerable<Meal>? meals { get; set; }
    }
}
