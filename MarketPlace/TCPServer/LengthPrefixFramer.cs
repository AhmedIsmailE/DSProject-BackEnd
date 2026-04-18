using System;
using System.Buffers;

namespace MarketPlace.Backend.TCPServer
{
    public class LengthPrefixFramer
    {
        /// <summary>
        /// Inspects the incoming byte stream to extract a complete message based on the 4-byte header.
        /// </summary>
        /// <param name="buffer">The current unread bytes in the pipeline.</param>
        /// <param name="payload">The extracted payload (if a full message is available).</param>
        /// <returns>True if a full message was parsed, False if we need to wait for more data.</returns>
        public bool TryParseFrame(ref ReadOnlySequence<byte> buffer, out byte[] payload)
        {
            payload = Array.Empty<byte>();

            // TODO:
            // 1. Check if buffer.Length >= 4. If not, return false.
            // 2. Slice the first 4 bytes and convert them back to an integer (payloadLength).
            // 3. Check if buffer.Length >= (4 + payloadLength). If not, return false (message is fragmented, wait for more).
            // 4. If we have the full message, slice the payload bytes out of the buffer.
            // 5. Update the 'buffer' reference to point past the consumed message (Advance the pipeline).
            // 6. Set 'out payload' to the extracted bytes and return true.

            return false;
        }
    }
}