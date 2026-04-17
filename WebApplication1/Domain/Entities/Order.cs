namespace WebApplication1.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public int BuyerId { get; set; }
        public int SellerId { get; set; }

        public int ProductId { get; set; }

        public DateTime Date { get; set; }

        // ADD THIS: The price of the product at the exact moment it was bought.
        public decimal PurchasePrice { get; set; }
    }
}
