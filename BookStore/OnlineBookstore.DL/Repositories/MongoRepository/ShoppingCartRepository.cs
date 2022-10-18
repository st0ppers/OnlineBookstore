using BookStore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.MongoRepository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IOptionsMonitor<MongoDbConfiguration> _settings;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<ShoppingCart> _collection;

        public ShoppingCartRepository(IOptionsMonitor<MongoDbConfiguration> settings, IPurchaseRepository purchaseRepository)
        {
            _settings = settings;
            _purchaseRepository = purchaseRepository;
            _client = new MongoClient(_settings.CurrentValue.ConnectionString);
            _database = _client.GetDatabase(_settings.CurrentValue.DatabaseName);
            _collection = _database.GetCollection<ShoppingCart>(_settings.CurrentValue.Shop);
        }
        public async Task<IEnumerable<ShoppingCart>> GetContent(int userId)
        {
            var shoppingCart = await _collection.FindAsync(x => x.UserId == userId);
            return shoppingCart.ToList();
        }

        public async Task<ShoppingCart> AddToCard(ShoppingCart cart)
        {
            var shoppingCart = await _collection.FindAsync(x => x.UserId == cart.UserId);
            if (shoppingCart == null)
            {
                return null;
            }
            await _collection.DeleteOneAsync(x => x.UserId == cart.UserId);
            await _collection.InsertOneAsync(cart);
            return cart;
        }

        public async Task<ShoppingCart> RemoveFromCart(ShoppingCart cart)
        {
            var shoppingCart = await _collection.FindAsync(x => x.UserId == cart.UserId);
            await _collection.DeleteOneAsync(x => x.UserId == cart.UserId);
            await _collection.InsertOneAsync(cart);
            return shoppingCart.Current.GetEnumerator().Current;
        }

        public Task EmptyCart(int userId)
        {
            _collection.DeleteMany(x => x.UserId == userId);
            return Task.CompletedTask;
        }

        public async Task FinishPurchase(Purchase purchase)
        {
            await _purchaseRepository.SavePurchase(purchase);
        }
    }
}
