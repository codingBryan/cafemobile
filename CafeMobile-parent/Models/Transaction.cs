

namespace CafeMobile_parent.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int? ParentId { get; set; }
        public Parent? Parent { get; set; }
        public double amount { get; set; }
        public string purpose { get; set; }
        public DateTime created_at { get; set; }
    }
}
