
namespace CafeMobile_parent.Models
{
    public class Parent
    {

        public int ParentId { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string phone_number { get; set; }
        public string email { get; set; }
        public byte[] password_hash { get; set; }
        public IEnumerable<Student>? students { get; set; }
        public IEnumerable<Transaction>? transactions { get; set; }
    }
}
