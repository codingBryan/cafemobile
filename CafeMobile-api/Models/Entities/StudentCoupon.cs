namespace CafeMobile_api.Models.Entities
{
    public class StudentCoupon
    {
        public int Id { get; set; }
        public double price { get; set; }
        public string? image { get; set; }
        public int CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public double balance { get; set; }
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public bool is_active { get; set; } = true;
    }
}
