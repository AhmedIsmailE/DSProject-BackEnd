using System.Net.Sockets;

namespace WebApplication1.Client.Networking
{
    public class SocketClient
    {
        private TcpClient _client;

        public void Connect(string ip, int port)
        {
            // Connect to server
        }

        public void Send(string message)
        {
            // Send message to server
        }

        public string Receive()
        {
            // Read response from server
            return "";
        }
    }
}
