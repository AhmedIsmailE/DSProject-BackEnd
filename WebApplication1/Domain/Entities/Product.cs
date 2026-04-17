namespace WebApplication1.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }

        public decimal Price { get; set; }

        public int OwnerId { get; set; }

        // ADD THIS: Required for the inventory management feature
        public int Quantity { get; set; }
    }
}
