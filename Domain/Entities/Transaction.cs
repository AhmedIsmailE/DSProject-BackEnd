using System;

namespace MarketPlace.Domain.Entities
{
    /// <summary>
    /// Represents the historical record of a purchase. Essential for the reporting requirement.
    /// </summary>
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public Guid ItemId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }

        // Important for the Saga pattern: Pending, Completed, or Failed/Reverted
        public string Status { get; set; } = "Pending";
    }
}