using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Repositories;

namespace MarketPlace.Infrastructure.Data.Mock
{
    public class MockItemRepository : IItemRepository
    {
        public Task<Item?> GetByIdAsync(Guid id)
        {
            MockDatabaseContext.Items.TryGetValue(id, out var item);
            return Task.FromResult(item);
        }

        public Task AddAsync(Item item)
        {
            MockDatabaseContext.Items.TryAdd(item.Id, item);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Item item)
        {
            MockDatabaseContext.Items.AddOrUpdate(item.Id, item, (key, oldValue) => item);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            MockDatabaseContext.Items.TryRemove(id, out _);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Item>> SearchAvailableItemsAsync(string searchTerm)
        {
            // TODO: Filter the Items dictionary where IsAvailable == true 
            // and Name or Description contains the searchTerm.
            var results = MockDatabaseContext.Items.Values
                .Where(i => i.IsAvailable && i.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult<IEnumerable<Item>>(results);
        }

        public Task<IEnumerable<Item>> GetItemsByOwnerIdAsync(Guid ownerId)
        {
            // TODO: Filter items by OwnerId
            var results = MockDatabaseContext.Items.Values
                .Where(i => i.OwnerId == ownerId);

            return Task.FromResult<IEnumerable<Item>>(results);
        }
    }
}