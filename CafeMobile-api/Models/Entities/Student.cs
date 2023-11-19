namespace CafeMobile_api.Models.Entities
{
    public class Student
    {
        public int StudentId { get; set; }
        public string adm_no { get; set; }
        public string? profile_pic { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; } = String.Empty;
        public string email { get; set; }
        public string phone_number { get; set; }
        public double cp_balance { get; set; } = 0;
        public string password_hash { get; set; }
        public IEnumerable<Parents_Students>? parents { get; set; }
        public IEnumerable<StudentCoupon>? coupons { get; set; }
        public IEnumerable<Transaction>? transactions { get; set; }
        public IEnumerable<Redemption>? redemptions { get; set; }
        public IEnumerable<Sale>? sales { get; set; }


    }
}
