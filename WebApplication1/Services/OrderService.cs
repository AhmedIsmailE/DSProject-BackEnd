using WebApplication1.Shared.Models;

namespace WebApplication1.Services
{
    public class OrderService
    {
        // IMPORTANT (USE LOCK) TO AVOID CONCURRUNCY
        // ADD THIS: Purchase item feature
        public bool PurchaseItem(int buyerId, int productId)
        {
            // 1. Find product in MockDatabase.Products
            // 2. Check if product.Quantity > 0
            // 3. Find buyer in MockDatabase.Users
            // 4. Check if buyer.Balance >= product.Price
            // 5. Deduct money from buyer, add money to seller (product.OwnerId)
            // 6. Reduce product.Quantity by 1
            // 7. Create new Order (saving the current product.Price to Order.PurchasePrice)
            // 8. Add Order to MockDatabase.Orders
            // 9. Return true if successful
            throw new NotImplementedException();
        }
    }
}
