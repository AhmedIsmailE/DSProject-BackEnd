namespace MarketPlace.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public decimal WalletBalance { get; set; }

        // Navigation properties for EF Core later
        // public ICollection<Item> Inventory { get; set; } = new List<Item>();
    }
}
