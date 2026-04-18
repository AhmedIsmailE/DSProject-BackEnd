using System;
using System.Threading.Tasks;
using MarketPlace.Application.DTOs;
using MarketPlace.Domain.Repositories;

namespace MarketPlace.Application.Queries
{
    public class SearchItemsQueryHandler
    {
        private readonly IItemRepository _itemRepository;

        public SearchItemsQueryHandler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<JsonEnvelope> HandleAsync(JsonEnvelope request)
        {
            // TODO:
            // 1. Deserialize request.Payload to get the search terms (e.g., keyword, max price).
            // 2. Await _itemRepository.SearchAvailableItemsAsync(searchTerm).
            // 3. Serialize the search results array into JSON.
            // 4. Return a "SEARCH_RESULTS" JsonEnvelope.

            return new JsonEnvelope
            {
                CorrelationId = request.CorrelationId,
                Command = "SEARCH_ERROR",
                Payload = "{\"message\": \"Not implemented yet\"}"
            };
        }
    }
}