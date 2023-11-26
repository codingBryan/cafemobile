using System.Collections.ObjectModel;

namespace CafeMobile.Models.DTOs
{
    public class GetStudentCoupon
    {
        public int Id { get; set; }
        public int CouponId { get; set; }
        public string? image { get; set; }
        public string name { get; set; }
        public double balance { get; set; }
        public ObservableCollection<GetMealDTO> meals { get; set; }
        public bool is_active { get; set; } = true;
    }
}
