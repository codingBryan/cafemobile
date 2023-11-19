namespace CafeMobile_parent.Models.DTO
{
    public class BuyCP
    {
        public int studentId { get; set; }
        public double amount_cp { get; set; }
        public string transactionCode { get; set; }
        public double amount_ksh { get; set; }
        public DateTime created_At { get; set; }
    }
}
