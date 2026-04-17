using WebApplication1.Domain.Entities;
using WebApplication1.Shared.Models;

namespace WebApplication1.Services
{
    public class ProductService
    {
        public Response AddProduct(int userId, string name, string brand, decimal price)
        {
            // Add product to database
            return new Response();
        }

        public Response EditProduct(int productId, decimal newPrice)
        {
            // Update product info
            return new Response();
        }

        public Response DeleteProduct(int productId)
        {
            // Remove product
            return new Response();
        }

        public Response Search(string keyword)
        {
            // Search by name/brand
            return new Response();
        }
        // ADD THIS: Manage inventory feature
        public bool UpdateInventory(int productId, int newQuantity, int userId)
        {
            // 1. Find product in MockDatabase.Products
            // 2. Verify the product.OwnerId == userId (only the owner can change stock)
            // 3. Update the Quantity field
            // 4. Return true if successful
            throw new NotImplementedException();
        }

        // ADD THIS: Fulfills "Interface for other stores" via sockets
        public List<Product> GetProductsForExternalStores()
        {
            // 1. Return all products from MockDatabase where Quantity > 0
            throw new NotImplementedException();
        }
    }
}
