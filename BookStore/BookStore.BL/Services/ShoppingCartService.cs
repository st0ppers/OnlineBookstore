using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IShoppingCartRepository _cartRepository;

        public ShoppingCartService(IShoppingCartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IEnumerable<ShoppingCart>> GetContent(int userId)
        {
            return await _cartRepository.GetContent(userId);
        }

        public async Task<ShoppingCart> AddToCard(int userId, Book book)
        {
            return await _cartRepository.AddToCard(userId, book);
        }

        public async Task<ShoppingCart> RemoveFromCart(int userId,Book book)
        {
            return await _cartRepository.RemoveFromCart(userId,book);
        }

        public async Task EmptyCart(int userId)
        {
            await _cartRepository.EmptyCart(userId);
        }

        public async Task FinishPurchase(Purchase purchase)
        {
            await _cartRepository.FinishPurchase(purchase);
        }
    }
}
