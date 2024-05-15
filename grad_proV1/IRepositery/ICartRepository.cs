using grad_proV1.Models;

namespace grad_proV1.IRepositery
{
    public interface ICartRepository
    {
        Task<bool> AddToCartAsync(int productId, string userId);
        void RemoveFromCart(int productId, string userId);
        List<CartItem> GetUserCart(string userId);
        List<CartItem> GetCartItems();
        void Delete(int id);
    }
}
