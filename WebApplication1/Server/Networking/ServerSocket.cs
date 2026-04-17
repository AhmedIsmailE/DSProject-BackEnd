using System.Net.Sockets;

namespace WebApplication1.Server.Networking
{
    public class SocketServer
    {
        private int _port;
        private TcpListener _listener;

        public SocketServer(int port)
        {
            _port = port;
        }

        public void Start()
        {
            // Start listening on the given port
            // Accept incoming clients in a loop
            // For each client → create a new thread/task

            // while(true)
            //    var client = AcceptTcpClient()
            //    Task.Run(() => HandleClient(client));
        }

        private void HandleClient(TcpClient client)
        {
            // Get network stream
            // Continuously read messages from client
            // Pass message to RequestParser
            // Execute business logic
            // Send response back
        }
    }
}
