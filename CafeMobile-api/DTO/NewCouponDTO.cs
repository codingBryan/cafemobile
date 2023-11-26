namespace CafeMobile_api.DTO
{
    public class NewCouponDTO
    {
        public int CouponId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public string? image { get; set; }
        public IEnumerable<int> meal_Ids { get; set; }

    }
}
