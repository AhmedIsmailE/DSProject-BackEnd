using WebApplication1.Domain.Entities;

namespace WebApplication1.Data
{
    public static class MockDatabase
    {
        public static Dictionary<int, User> Users = new();
        public static Dictionary<int, Product> Products = new();
        public static List<Order> Orders = new();

        // Maps a SessionToken (string) to a UserId (int) to keep users logged in
        public static Dictionary<string, int> ActiveSessions = new();
    }
}
