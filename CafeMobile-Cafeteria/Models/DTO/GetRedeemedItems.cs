namespace Cafemobile_Cafeteria.Models.DTO
{
    public class GetRedeemedItems
    {
        public Guid RedemptionId { get; set; }
        public string student_name { get; set; }
        public bool scanned { get; set; } = false;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public IEnumerable<GetSoldMeal> meals { get; set; }
    }
}
