namespace CafeMobile_api.Models.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int studentId { get; set; }
        public Student Student { get; set; }
        public Guid RedemptionId { get; set; }
        public Redemption Redemption { get; set; }
        public int mealId { get; set; }
        public int units { get; set; }
        public double price { get; set; }
        public string created_at { get; set; }
        public bool pending { get; set; } = true;
        public bool cooking { get; set; } = false;
        public bool prepared { get; set; } = false;
    }
}
