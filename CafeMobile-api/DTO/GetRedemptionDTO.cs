using CafeMobile_api.Models.Entities;

namespace CafeMobile_api.DTO
{
    public class GetRedemptionDTO
    {
        public Guid RedemptionId { get; set; }
        public bool scanned { get; set; } = false;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public IEnumerable<GetMealDTO> meals { get; set; }
    }
}
