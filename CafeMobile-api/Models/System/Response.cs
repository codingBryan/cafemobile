using System.Net;

namespace CafeMobile_api.Models.System
{
    public class Response<T>
    {
        public T? data { get; set; }
        public string message { get; set; } = String.Empty;
        public bool success { get; set; } = true;
        public HttpStatusCode status_code { get; set; }
    }


}
