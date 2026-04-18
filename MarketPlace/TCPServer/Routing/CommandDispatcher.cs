using MarketPlace.Application.DTOs;
using System;
using System.Threading.Tasks;
// using Marketplace.Application.Commands; // Add reference when implemented

namespace MarketPlace.Backend.TCPServer.Routing
{
    public class CommandDispatcher
    {
        // In a real implementation, you would inject an IServiceProvider here to resolve handlers dynamically

        public CommandDispatcher()
        {
        }

        /// <summary>
        /// Routes the incoming JSON envelope to the correct application handler.
        /// </summary>
        public async Task<JsonEnvelope> DispatchAsync(JsonEnvelope request)
        {
            // TODO:
            // 1. Switch on request.Command (e.g., case "LOGIN", case "PURCHASE_ITEM").
            // 2. Resolve the appropriate CommandHandler.
            // 3. Await handler.HandleAsync(request).
            // 4. Return the resulting JsonEnvelope to be sent back down the TCP socket.

            switch (request.Command.ToUpper())
            {
                case "PURCHASE_ITEM":
                    // return await _purchaseHandler.HandleAsync(request);
                    break;
                case "LOGIN":
                    // return await _loginHandler.HandleAsync(request);
                    break;
            }

            // Default fallback
            return new JsonEnvelope
            {
                CorrelationId = request.CorrelationId,
                Command = "UNKNOWN_COMMAND",
                Payload = "{}"
            };
        }
    }
}