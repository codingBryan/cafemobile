namespace CafeMobile_api.Models.Entities
{
    public class Redemption
    {
        public Guid RedemptionId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public bool scanned { get; set; } = false;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public DateTime? scanned_at { get; set; }
        public IEnumerable<Sale>? sales { get; set; }
        public IEnumerable<Redemption_meal>? meals { get; set; }

    }
}
