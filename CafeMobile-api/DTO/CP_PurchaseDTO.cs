namespace CafeMobile_api.DTO
{
    public class CP_PurchaseDTO
    {
        public int studentId { get; set; }
        public double amount_cp { get; set; }
        public string transactionCode { get; set; }
        public double amount_ksh { get; set; }
        public DateTime created_At { get; set; }
    }
}
