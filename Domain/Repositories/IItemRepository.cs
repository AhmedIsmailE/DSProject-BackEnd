using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities;

namespace MarketPlace.Domain.Repositories
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(Guid id);
        Task AddAsync(Item item);
        Task UpdateAsync(Item item);
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Used for Feature v: Searching for items for sale.
        /// </summary>
        Task<IEnumerable<Item>> SearchAvailableItemsAsync(string searchTerm);

        /// <summary>
        /// Used for Feature vii: Viewing items to be sold.
        /// </summary>
        Task<IEnumerable<Item>> GetItemsByOwnerIdAsync(Guid ownerId);
    }
}