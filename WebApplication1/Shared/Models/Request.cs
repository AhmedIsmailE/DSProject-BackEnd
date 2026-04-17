using System.Text.Json;
using WebApplication1.Shared.Enums;
namespace WebApplication1.Shared.Models
{
    public class Request
    {
        public RequestType Type { get; set; }

        // ADD THIS: Identifies the logged-in user
        public string SessionToken { get; set; }

        // EDIT THIS: Use JsonElement instead of Dictionary for easier object parsing
        public JsonElement Payload { get; set; }
    }
}
