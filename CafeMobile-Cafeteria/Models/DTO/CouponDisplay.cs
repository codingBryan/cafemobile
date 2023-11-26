namespace Cafemobile_Cafeteria.Models.DTO
{
    public class CouponDisplay
    {
        public int CouponId { get; set; }
        public string name { get; set; }
        public double price { get; set; }
        public double price_CP  => price / 10; 
        public double balance { get; set; }
        public string? image { get; set; }
        public bool is_active { get; set; }
        public ImageSource? displayImage { get; set; }
        public IEnumerable<GetMealDTO> meals { get; set; } = new List<GetMealDTO>();
    }
}
