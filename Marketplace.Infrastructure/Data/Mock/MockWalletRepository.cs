using System;
using System.Linq;
using System.Threading.Tasks;
using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Repositories;

namespace MarketPlace.Infrastructure.Data.Mock
{
    public class MockWalletRepository : IWalletRepository
    {
        public Task<Wallet?> GetByUserIdAsync(Guid userId)
        {
            var wallet = MockDatabaseContext.Wallets.Values.FirstOrDefault(w => w.UserId == userId);
            return Task.FromResult(wallet);
        }

        public Task UpdateAsync(Wallet wallet)
        {
            MockDatabaseContext.Wallets.AddOrUpdate(wallet.Id, wallet, (key, old) => wallet);
            return Task.CompletedTask;
        }
    }
}