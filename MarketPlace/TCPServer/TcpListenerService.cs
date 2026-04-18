using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MarketPlace.Backend.TCPServer
{
    /// <summary>
    /// Runs continuously in the background alongside your Kestrel web server.
    /// </summary>
    public class TcpListenerService : BackgroundService
    {
        private readonly ILogger<TcpListenerService> _logger;
        private Socket? _listenerSocket;
        private readonly int _port = 9000; // The port the Gateway connects to

        public TcpListenerService(ILogger<TcpListenerService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listenerSocket.Bind(new IPEndPoint(IPAddress.Any, _port));
            _listenerSocket.Listen(100);

            _logger.LogInformation($"TCP Server listening on port {_port}");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Asynchronously accept incoming connections from the Gateway without blocking
                    var clientSocket = await _listenerSocket.AcceptAsync(stoppingToken);

                    _logger.LogInformation($"Client connected: {clientSocket.RemoteEndPoint}");

                    // Fire and forget the connection processor so the listener can immediately accept the next client
                    _ = Task.Run(() => ProcessConnectionAsync(clientSocket, stoppingToken), stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    // Server is shutting down gracefully
                    break;
                }
            }
        }

        private async Task ProcessConnectionAsync(Socket socket, CancellationToken cancellationToken)
        {
            // TODO: 
            // 1. Instantiate your ConnectionProcessor class here.
            // 2. Pass the socket to it so it can start the System.IO.Pipelines read/write loops.

            await Task.CompletedTask;
        }
    }
}