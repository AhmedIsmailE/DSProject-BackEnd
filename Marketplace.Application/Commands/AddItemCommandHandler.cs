using System;
using System.Threading.Tasks;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Repositories;
// using MarketPlace.Domain.Entities; // Needed later when creating the Item

namespace MarketPlace.Application.Commands
{
    public class AddItemCommandHandler
    {
        private readonly IItemRepository _itemRepository;

        public AddItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<JsonEnvelope> HandleAsync(JsonEnvelope request)
        {
            // TODO:
            // 1. Deserialize request.Payload into an Item DTO (Name, Description, Price, OwnerId).
            // 2. Validate the data (e.g., price must be > 0).
            // 3. Map the DTO to a Domain Entity: var newItem = new Item { ... };
            // 4. Await _itemRepository.AddAsync(newItem).
            // 5. Construct and return an "ITEM_ADDED" JsonEnvelope.

            return new JsonEnvelope
            {
                CorrelationId = request.CorrelationId,
                Command = "ADD_ITEM_FAILED",
                Payload = "{\"message\": \"Not implemented yet\"}"
            };
        }
    }
}