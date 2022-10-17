using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IShoppingCartService
    {
        public Task<IEnumerable<ShoppingCart>> GetContent(int userId);
        public Task<ShoppingCart> AddToCard(int userId, Book book);
        public Task<ShoppingCart> RemoveFromCart(int userId, Book book);
        public Task EmptyCart(int userId);
        public Task FinishPurchase(Purchase purchase);
    }
}
