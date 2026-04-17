namespace WebApplication1.Shared.Models
{
    public class Response
    {
        public string Status { get; set; } // OK / ERROR
        public string Message { get; set; }

        public object Data { get; set; }
    }
}
