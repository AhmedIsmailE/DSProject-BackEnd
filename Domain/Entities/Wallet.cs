using System;

namespace MarketPlace.Domain.Entities
{
    /// <summary>
    /// While we put 'WalletBalance' directly on the User object earlier for simplicity, 
    /// having a dedicated Wallet entity is much better for the distributed Saga pattern 
    /// when we need to lock funds independently of the user profile.
    /// </summary>
    public class Wallet
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }

        // You could also add fields here like "ReservedBalance" for when a purchase is pending.
    }
}