namespace CafeMobile_api.DTO
{
    public class GetSoldItem
    {
        public int MealId { get; set; }
        public int saleId { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public int quantity { get; set; }
        public string? image { get; set; }
        public bool pending { get; set; } = true;
        public bool cooking { get; set; } = false;
        public bool prepared { get; set; } = false;
    }
}
