using WebApplication1.Shared.Models;

namespace WebApplication1.Services
{
    public class UserService
    {
        public Response Register(string username, string password)
        {
            //just ensure it returns a generated SessionToken
            // Check if user exists
            // Add new user
            return new Response();
        }

        public Response Login(string username, string password)
        {
            // Validate credentials
            // Return user sessiontoken info
            return new Response();
        }

        // EDIT/ADD: Needs to handle depositing cash
        public bool DepositCash(int userId, decimal amount)
        {
            // 1. Find user in MockDatabase.Users
            // 2. Add amount to their balance
            // 3. Return true if successful
            throw new NotImplementedException();
        }

        // EDIT/ADD: View account info, balance, and lists (sold/bought)
        public object GetProfile(int userId)
        {
            // 1. Find user in MockDatabase.Users
            // 2. Find all products they own in MockDatabase.Products (Items for sale)
            // 3. Find all orders they made in MockDatabase.Orders (Purchased items)
            // 4. Find all orders others made FOR their products (Sold items)
            // 5. Return an object containing all this data
            throw new NotImplementedException();
        }
    }
}
