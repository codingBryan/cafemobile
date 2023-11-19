namespace CafeMobile_api.Models.Entities
{
    public class Parents_Students
    {
        public int parentId { get; set; }
        public Parent Parent { get; set; }
        public int studentId { get; set; }
        public Student Student { get; set; }
    }
}
