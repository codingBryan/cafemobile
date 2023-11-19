namespace CafeMobile_api.DTO
{
    public class GetRedemptionCafeteria
    {
        public Guid RedemptionId { get; set; }
        public string student_name { get; set; }
        public bool scanned { get; set; } = false;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public IEnumerable<GetSoldItem> meals { get; set; }
    }
}
