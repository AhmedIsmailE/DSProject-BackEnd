using MarketPlace.Domain.Repositories;
using MarketPlace.Infrastructure.Data.Mock;
using Microsoft.Extensions.DependencyInjection;

namespace MarketPlace.Infrastructure
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Extension method to register all infrastructure services.
        /// Call this in your Backend's Program.cs.
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Phase 1: Register the Mock Repositories
            // We use Singleton here because our mock database is just a static dictionary.
            // If we used Transient, it would wipe the data on every request!
            services.AddSingleton<IUserRepository, MockUserRepository>();
            services.AddSingleton<IItemRepository, MockItemRepository>();
            services.AddSingleton<IWalletRepository, MockWalletRepository>();
            services.AddSingleton<ITransactionRepository, MockTransactionRepository>();

            // Phase 2 (Future): When you build your real distributed DB, you will 
            // comment out the lines above and uncomment the lines below.
            // services.AddScoped<IUserRepository, SqlUserRepository>();
            // services.AddScoped<IItemRepository, SqlItemRepository>();
            // etc...

            return services;
        }
    }
}