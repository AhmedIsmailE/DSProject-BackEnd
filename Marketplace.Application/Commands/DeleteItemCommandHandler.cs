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
    public class DeleteItemCommandHandler
    {
        private readonly IItemRepository _itemRepository;

        public DeleteItemCommandHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<JsonEnvelope> HandleAsync(JsonEnvelope request)
        {
            // 1. Deserialize request payload into DTO
            var payload = JsonSerializer.Deserialize<DeleteItemPayload>(request.Payload);

            if (payload is null || payload.ItemId == Guid.Empty)
            {
                return new JsonEnvelope
                {
                    Command = "DELETE_ITEM_FAILED",
                    CorrelationId = request.CorrelationId,
                    Payload = JsonSerializer.Serialize(new
                    {
                        Message = "Invalid payload"
                    })
                };
            }

            // 2. Get item from repository
            var item = await _itemRepository.GetByIdAsync(payload.ItemId) ?? throw new ItemNotFoundException(payload.ItemId);

            // 3.1 Soft delete (option 1) - mark item as unavailable
            //item.IsAvailable = false;
            //await _itemRepository.UpdateAsync(item);

            // 3.2 Hard delete (option 2) - remove item from repository
            await _itemRepository.DeleteAsync(payload.ItemId);

            if (item.OwnerId != payload.RequestingUserId)
            {
                return new JsonEnvelope
                {
                    Command = "DELETE_ITEM_FAILED",
                    CorrelationId = request.CorrelationId,
                    Payload = JsonSerializer.Serialize(new { Success = false, Message = "Unauthorized" })
                };
            }
            // 4. Return success response
            return new JsonEnvelope
            {
                Command = "DELETE_ITEM_SUCCESS",
                CorrelationId = request.CorrelationId,
                Payload = JsonSerializer.Serialize(new
                {
                    Success = true,
                    ItemId = item.Id
                })
            };
        }
    }
    public record DeleteItemPayload(Guid ItemId, Guid RequestingUserId);
}
