using grad_proV1.Data;
using grad_proV1.IRepositery;
using grad_proV1.Models;
using Microsoft.EntityFrameworkCore;

namespace grad_proV1.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext context;

        public CartRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public List<CartItem> GetCartItems()
        {
            var items = context.CartItems.ToList();
            return items;
        }
        //public void AddToCart(int productId, string userId)
        //{
        //    // Check if item exists in cart, update quantity if so
        //    //var existingItem = context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
        //    //if (existingItem != null)
        //    //{
        //    //    existingItem.Quantity++;
        //    //}
        //    //else
        //   // {
        //       var finditem = context.products.Find(productId);
        //        if(finditem )
        //        {
        //            context.CartItems.Add(new CartItem { ProductId =productId, UserId = userId, Quantity = 1 });
        //            context.SaveChanges();
        //       }
               
        //   // }
         
        //}
        public async Task<bool> AddToCartAsync(int productId,string userId)
        {
            // Check if product exists in database using asynchronous query
            var productExists = await context.products.AnyAsync(p => p.Id == productId);
            if (!productExists)
            {
                // Inform user that product is not available (optional)
                // throw new Exception("Product not found in database."); // Uncomment for stricter error handling
                return false; // Indicate unsuccessful addition
            }

            // Proceed with cart logic (assuming asynchronous operations for performance)
            var existingItem = await context.CartItems.Where(e => e.UserId == userId)
                .FirstOrDefaultAsync(c => c.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity++;
                context.SaveChanges();
            }
            else
            {
                context.CartItems.Add(new CartItem { ProductId = productId, UserId =userId , Quantity = 1 });
             //   "3c9e9e0d-485d-4565-9fcb-0b89be118751"
            }

            context.SaveChanges();
            return true; // Indicate successful addition
        }
        public void RemoveFromCart(int productId, string userId)
        {
            var itemToRemove = context.CartItems.FirstOrDefault(ci => ci.ProductId == productId && ci.UserId == userId);
            if (itemToRemove != null)
            {
                context.CartItems.Remove(itemToRemove);
                context.SaveChanges();
            }
        }

        public List<CartItem> GetUserCart(string userId)
        {
            return context.CartItems.Where(ci => ci.UserId == userId).ToList();
        }
        public void Delete(int id)
        {
            var deletedcart = context.CartItems.Find(id);


            context.Remove(deletedcart);
            context.SaveChanges();
        }
    }
}
