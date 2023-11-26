namespace CafeMobile.Models.DTOs
{
    public class CouponRedemption
    {
        public int CouponId { get; set; }
        public double redemptionTotal { get; set; }
        public IEnumerable<RedeemMealDTO> meals { get; set; }
    }
}
