namespace CafeMobile_api.DTO
{
    public class GetStudentCouponDTO
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public string? image { get; set; }
        public string name { get; set; }
        public double balance { get; set; }
        public bool is_active { get; set; } = true;
        public IEnumerable<GetMealDTO> meals { get; set; }
    }
}
