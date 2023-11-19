using System.Net;

namespace CafeMobile_parent.Models.System
{
    public class Response <T>
    {
        public T data { get; set; }
        public bool success { get; set; }
        public string message { get; set; }
        public HttpStatusCode status_code { get; set; }
    }
}
