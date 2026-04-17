using System.Net.Sockets;

namespace WebApplication1.Server.Networking
{
    public class ClientHandler
    {
        private TcpClient _client;

        public ClientHandler(TcpClient client)
        {
            _client = client;
        }

        public void Process()
        {
            // Loop:
            // 1. Read message from client
            // 2. Convert bytes → string
            // 3. Send to protocol parser
            // 4. Get response
            // 5. Send response back
        }

        private string ReadMessage()
        {
            // Read from NetworkStream
            // Handle partial messages
            return "";
        }

        private void SendMessage(string message)
        {
            // Convert string → bytes
            // Write to NetworkStream
        }
    }
}
