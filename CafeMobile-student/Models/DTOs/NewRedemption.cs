

namespace CafeMobile.Models.DTOs
{
    public class NewRedemption
    {
        public double total { get; set; }
        public IEnumerable<RedeemMealDTO>? meals { get; set; }
    }
}
