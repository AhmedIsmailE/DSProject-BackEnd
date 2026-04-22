using MarketPlace.Application.Commands;
using MarketPlace.Application.DTOs;
using System.Text.Json;
using System;
using System.Threading.Tasks;
// using Marketplace.Application.Commands; // Add reference when implemented

namespace MarketPlace.Backend.TCPServer.Routing
{
    public class CommandDispatcher
    {
        private readonly LoginCommandHandler _loginCommandHandler;
        private readonly CreateAccountCommandHandler _createAccountCommandHandler;
        private readonly PurchaseItemCommandHandler _purchaseItemCommandHandler;
        private readonly DepositCashCommandHandler _depositCashCommandHandler;
        private readonly AddItemCommandHandler _addItemCommandHandler;

        public CommandDispatcher(
            LoginCommandHandler loginCommandHandler,
            CreateAccountCommandHandler createAccountCommandHandler,
            PurchaseItemCommandHandler purchaseItemCommandHandler,
            DepositCashCommandHandler depositCashCommandHandler,
            AddItemCommandHandler addItemCommandHandler)
        {
            _loginCommandHandler = loginCommandHandler;
            _createAccountCommandHandler = createAccountCommandHandler;
            _purchaseItemCommandHandler = purchaseItemCommandHandler;
            _depositCashCommandHandler = depositCashCommandHandler;
            _addItemCommandHandler = addItemCommandHandler;
        }

        /// <summary>
        /// Routes the incoming JSON envelope to the correct application handler.
        /// </summary>
        public async Task<JsonEnvelope> DispatchAsync(JsonEnvelope request)
        {
            if (request == null)
            {
                return BuildResponse(
                    string.Empty,
                    "INVALID_REQUEST",
                    new
                    {
                        Success = false,
                        Message = "Request is null."
                    });
            }

            switch (request.Command?.ToUpperInvariant())
            {
                case "PURCHASE_ITEM":
                    return await _purchaseItemCommandHandler.HandleAsync(request);
                case "LOGIN":
                    return await _loginCommandHandler.HandleAsync(request);
                case "CREATE_ACCOUNT":
                    return await _createAccountCommandHandler.HandleAsync(request);
                case "DEPOSIT_CASH":
                    return await _depositCashCommandHandler.HandleAsync(request);
                case "ADD_ITEM":
                    return await _addItemCommandHandler.HandleAsync(request);
                default:
                    return BuildResponse(
                        request.CorrelationId,
                        "UNKNOWN_COMMAND",
                        new
                        {
                            Success = false,
                            Message = $"Command '{request.Command}' is not recognized."
                        });
            }
        }
        private static JsonEnvelope BuildResponse(string correlationId, string command, object responsePayload)
        {
            return new JsonEnvelope
            {
                CorrelationId = correlationId,
                Command = command,
                Payload = JsonSerializer.Serialize(responsePayload)
            };
        }
    }
}