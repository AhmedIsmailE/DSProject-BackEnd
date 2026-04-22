using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Exceptions;
using MarketPlace.Domain.Repositories;
using System.Text.Json;

namespace MarketPlace.Application.Commands
{
    public class EditItemCommandHandler
    {
        private readonly IItemRepository _itemRepository;

        public EditItemCommandHandler(IItemRepository items)
        {
            _itemRepository = items;
        }

        public async Task<JsonEnvelope> HandleAsync(JsonEnvelope request)
        {
            // 1. Deserialize request.Payload into EditItemPayload DTO
            var payload = JsonSerializer.Deserialize<EditItemPayload>(request.Payload);

            if (payload is null || payload.ItemId == Guid.Empty)
            {
                return new JsonEnvelope
                {
                    Command = "EDIT_ITEM_FAILED",
                    CorrelationId = request.CorrelationId,
                    Payload = JsonSerializer.Serialize(new { Success = false, Message = "Invalid payload" })
                };
            }

            // 2. Retrieve item from repository
            var item = await _itemRepository.GetByIdAsync(payload.ItemId) ?? throw new ItemNotFoundException(payload.ItemId);

            // 3. Update only fields that were provided (partial update)
            item.Name = payload.Name ?? item.Name;
            item.Description = payload.Description ?? item.Description;
            item.Price = payload.Price ?? item.Price;
            item.IsAvailable = payload.IsAvailable ?? item.IsAvailable;

            // 4. Save updated item
            await _itemRepository.UpdateAsync(item);

            if (item.OwnerId != payload.RequestingUserId)
            {
                return new JsonEnvelope
                {
                    Command = "EDIT_ITEM_FAILED",
                    CorrelationId = request.CorrelationId,
                    Payload = JsonSerializer.Serialize(new { Success = false, Message = "Unauthorized" })
                };
            }
            // 5. Return success response envelope
            return new JsonEnvelope
            {
                Command = "EDIT_ITEM_SUCCESS",
                CorrelationId = request.CorrelationId,
                Payload = JsonSerializer.Serialize(new
                {
                    Success = true,
                    ItemId = item.Id
                })
            };
        }
    }

    public record EditItemPayload(
        Guid ItemId,
        Guid RequestingUserId,
        string? Name,
        string? Description,
        decimal? Price,
        bool? IsAvailable
    );
}
