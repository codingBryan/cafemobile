using System.Net;

namespace CafeMobile.Models
{
    public class Response<T>
    {
        public T? data { get; set; }
        public string message { get; set; } = String.Empty;
        public bool success { get; set; }
        public HttpStatusCode status_code { get; set; }
    }
}
