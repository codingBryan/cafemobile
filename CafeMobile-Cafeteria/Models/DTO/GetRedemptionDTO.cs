namespace Cafemobile_Cafeteria.Models.DTO
{
    public class GetRedemptionDTO
    {
        public Guid RedemptionId { get; set; }
        public string student_name { get; set; }
        public bool scanned { get; set; } = false;
        public DateTime created_at { get; set; } = DateTime.UtcNow;
        public IEnumerable<GetMealDTO> meals { get; set; }
    }
}
