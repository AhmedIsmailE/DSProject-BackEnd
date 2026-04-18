using MarketPlace.Domain.Entities;
using MarketPlace.Domain.Repositories;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Marketplace.Infrastructure.Data.Mock
{
    public class MockUserRepository : IUserRepository
    {
        // Thread-safe dictionary to act as our database for now
        private static readonly ConcurrentDictionary<Guid, User> _users = new();

        public Task<User?> GetByIdAsync(Guid id)
        {
            // TODO: Attempt to get the user from the concurrent dictionary
            _users.TryGetValue(id, out var user);
            return Task.FromResult(user);
        }

        public Task<User?> GetByUsernameAsync(string username)
        {
            // TODO: Iterate through dictionary to find matching username
            // Return null if not found
            return Task.FromResult<User?>(null);
        }

        public Task UpdateAsync(User user)
        {
            // TODO: Update the dictionary entry with the new user object
            _users.AddOrUpdate(user.Id, user, (key, oldValue) => user);
            return Task.CompletedTask;
        }
    }
}