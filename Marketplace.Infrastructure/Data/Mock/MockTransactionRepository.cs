using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Repositories;

namespace MarketPlace.Infrastructure.Data.Mock
{
    public class MockTransactionRepository : ITransactionRepository
    {
        public Task AddAsync(Transaction transaction)
        {
            MockDatabaseContext.Transactions.TryAdd(transaction.Id, transaction);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Transaction transaction)
        {
            MockDatabaseContext.Transactions.AddOrUpdate(transaction.Id, transaction, (key, old) => transaction);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Transaction>> GetAllTransactionsForReportAsync()
        {
            return Task.FromResult<IEnumerable<Transaction>>(MockDatabaseContext.Transactions.Values);
        }
    }
}