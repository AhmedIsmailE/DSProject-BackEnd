namespace WebApplication1.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public decimal Balance { get; set; }

        public List<Product> OwnedProducts { get; set; }
    }
}
