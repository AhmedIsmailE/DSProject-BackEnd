using System;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace MarketPlace.Backend.TCPServer
{
    public class ConnectionProcessor
    {
        private readonly Socket _socket;
        // private readonly LengthPrefixFramer _framer;
        // private readonly CommandDispatcher _dispatcher;

        public ConnectionProcessor(Socket socket)
        {
            _socket = socket;
        }

        /// <summary>
        /// Manages the high-performance memory pipelines for this specific socket connection.
        /// </summary>
        public async Task StartAsync()
        {
            var pipe = new Pipe();

            // Task 1: Read raw bytes from the socket into the PipeWriter
            var fillPipeTask = FillPipeAsync(_socket, pipe.Writer);

            // Task 2: Read bytes from the PipeReader, frame them, and dispatch them
            var readPipeTask = ReadPipeAsync(pipe.Reader);

            // Wait for the connection to end
            await Task.WhenAll(fillPipeTask, readPipeTask);
        }

        private async Task FillPipeAsync(Socket socket, PipeWriter writer)
        {
            // TODO:
            // 1. Request a Memory<byte> buffer from the writer (writer.GetMemory()).
            // 2. Await socket.ReceiveAsync() into that memory.
            // 3. Call writer.Advance(bytesRead).
            // 4. Call await writer.FlushAsync() to notify the reader that data is available.
            // 5. Loop until socket is closed.
            await Task.CompletedTask;
        }

        private async Task ReadPipeAsync(PipeReader reader)
        {
            // TODO:
            // 1. Await reader.ReadAsync() to get the ReadResult.
            // 2. Pass the result.Buffer to your LengthPrefixFramer.
            // 3. If a full message is framed, pass it to the CommandDispatcher.
            // 4. Call reader.AdvanceTo() to tell the pipeline how much memory was consumed.
            // 5. Loop until stream is complete.
            await Task.CompletedTask;
        }
    }
}