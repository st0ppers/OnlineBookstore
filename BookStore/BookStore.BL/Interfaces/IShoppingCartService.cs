using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IShoppingCartService
    {
        public Task<IEnumerable<ShoppingCart>> GetContent(int userId);
        public Task<ShoppingCart> AddToCard(ShoppingCart cart);
        public Task<ShoppingCart> RemoveFromCart(ShoppingCart cart);
        public Task EmptyCart(int userId);
        public Task FinishPurchase(Purchase purchase);
    }
}
