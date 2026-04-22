using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Repositories;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities; 

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
            // 1. Deserialize request.Payload into an Item DTO (Name, Description, Price, OwnerId).
            var payload = JsonSerializer.Deserialize<AddItemPayload>(request.Payload);

            // 2. Validate the data.
            if (payload is null || payload.OwnerId == Guid.Empty)
            {
                return new JsonEnvelope
                {
                    CorrelationId = request.CorrelationId,
                    Command = "ADD_ITEM_FAILED",
                    Payload = JsonSerializer.Serialize(new { Message = "Invalid payload" })
                };
            }
            if (payload.Price <= 0)
            {
                return new JsonEnvelope
                {
                    CorrelationId = request.CorrelationId,
                    Command = "ADD_ITEM_FAILED",
                    Payload = JsonSerializer.Serialize(new { Message = "Price must be greater than 0" })
                };
            }
            if (string.IsNullOrWhiteSpace(payload.Name))
            {
                return new JsonEnvelope
                {
                    CorrelationId = request.CorrelationId,
                    Command = "ADD_ITEM_FAILED",
                    Payload = JsonSerializer.Serialize(new { Message = "Name is required" })
                };
            }

            // 3. Map the DTO to a Domain Entity;
            var item = new Item
            {
                ItemId = 0, // Assuming the repository will assign an ID
                StoreId = 0, // This would be set based on the OwnerId in a real implementation
                Name = payload.Name,
                Price = payload.Price,
                Brand = null, // Optional field
                Description = payload.Description,
                StockQuantity = 0, // Default stock quantity
                ImageUrl = null, // Optional field
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = 0,
            };

            // 4. Await _itemRepository.AddAsync(newItem).
            await _itemRepository.AddAsync(item);

            // 5. Construct and return an "ITEM_ADDED" JsonEnvelope.
            return new JsonEnvelope
            {
                CorrelationId = request.CorrelationId,
                Command = "ITEM_ADDED",
                Payload = JsonSerializer.Serialize(new
                {
                    Success = true,
                    ItemId = item.ItemId
                })
            };
        }
    }
    public record AddItemPayload(Guid OwnerId,
        string Name,
        string Description,
        decimal Price
    );
}