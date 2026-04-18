using System;
using System.Threading.Tasks;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Repositories;

namespace MarketPlace.Application.Queries
{
    public class GetUserInventoryQueryHandler
    {
        private readonly IItemRepository _itemRepository;

        public GetUserInventoryQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<JsonEnvelope> HandleAsync(JsonEnvelope request)
        {
            // TODO:
            // 1. Deserialize request.Payload to get the UserId.
            // 2. Await _itemRepository.GetItemsByOwnerIdAsync(userId).
            // 3. Serialize the list of items into a JSON string.
            // 4. Return an "INVENTORY_RESULTS" JsonEnvelope containing the serialized array.

            return new JsonEnvelope
            {
                CorrelationId = request.CorrelationId,
                Command = "INVENTORY_ERROR",
                Payload = "{\"message\": \"Not implemented yet\"}"
            };
        }
    }
}