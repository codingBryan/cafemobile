namespace CafeMobile_api.Models.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string audience { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}
