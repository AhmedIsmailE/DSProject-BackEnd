using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Marketplace.Gateway.TcpForwarding
{
    public class TcpClientWrapper
    {
        private readonly TcpClient _tcpClient;

        public TcpClientWrapper()
        {
            _tcpClient = new TcpClient();
        }

        /// <summary>
        /// Connects to the core C# backend TCP server.
        /// </summary>
        public async Task ConnectToBackendAsync(string host, int port)
        {
            // TODO: Connect the _tcpClient to the specified host and port
            await Task.CompletedTask;
        }

        /// <summary>
        /// Takes a payload received from a WebSocket, prepends the 4-byte length, and sends over TCP.
        /// </summary>
        public async Task ForwardToBackendAsync(byte[] webSocketPayload)
        {
            // TODO:
            // 1. Calculate length: int payloadLength = webSocketPayload.Length;
            // 2. Convert to 4 bytes: byte[] header = BitConverter.GetBytes(payloadLength);
            // 3. Ensure Network Byte Order (Big-Endian) for consistency.
            // 4. Write header + payload to _tcpClient.GetStream().

            await Task.CompletedTask;
        }
    }
}