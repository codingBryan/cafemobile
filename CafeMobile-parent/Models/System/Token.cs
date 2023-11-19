namespace CafeMobile_parent.Models.System
{
    public class Token <U>
    {
        public U user { get; set; }
        public string token { get; set; }
        public DateTime expires_at { get; set; }
    }
}
