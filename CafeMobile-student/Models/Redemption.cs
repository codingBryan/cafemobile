using CafeMobile.Models.DTOs;

namespace CafeMobile.Models
{
    public class Redemption
    {
        public Guid RedemptionId { get; set; }
        public bool scanned { get; set; } = false;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public IEnumerable<GetMealDTO>? meals { get; set; }
    }
}