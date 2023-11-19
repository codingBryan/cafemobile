using CafeMobile_api.Models.Entities;

namespace CafeMobile_api.DTO
{
    public class NewRedemptionDTO
    {
        public double total { get; set; }
        public IEnumerable<RedeemMealDTO>? meals { get; set; }

    }
}
