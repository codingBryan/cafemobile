namespace CafeMobile_api.Models.Entities
{
    public class ItemSales
    {
        public string ItemName { get; set; }
        public int unitsSold { get; set; } =0;
        public double totalSales { get; set; } = 0;
        public string sold_on { get; set; }
    }
}
