using System;

namespace MarketPlace.Domain.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; } // Links to the User who currently owns it
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        // Critical for preventing double-purchasing in a distributed system
        public bool IsAvailable { get; set; } = true;
    }
}