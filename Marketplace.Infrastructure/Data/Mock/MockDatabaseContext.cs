using System;
using System.Collections.Concurrent;
using MarketPlace.Domain.Entities;

namespace MarketPlace.Infrastructure.Data.Mock
{
    /// <summary>
    /// Acts as our temporary in-memory database. 
    /// All mock repositories will read and write from these shared static dictionaries.
    /// </summary>
    public static class MockDatabaseContext
    {
        public static readonly ConcurrentDictionary<Guid, User> Users = new();
        public static readonly ConcurrentDictionary<Guid, Item> Items = new();
        public static readonly ConcurrentDictionary<Guid, Wallet> Wallets = new();
        public static readonly ConcurrentDictionary<Guid, Transaction> Transactions = new();

        // Optional: You could add a static constructor here to "seed" the database 
        // with a test user and a test item so you have data to play with immediately!
    }
}