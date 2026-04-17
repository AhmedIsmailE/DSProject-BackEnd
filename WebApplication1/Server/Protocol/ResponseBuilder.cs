namespace WebApplication1.Server.Protocol
{
    public class ResponseBuilder
    {
        public string BuildSuccess(object data)
        {
            // Wrap data in a success response
            // Convert to JSON
            return "";
        }

        public string BuildError(string message)
        {
            // Build error response JSON
            return "";
        }
    }
}
