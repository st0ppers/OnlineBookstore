using BookStore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineBookstore.DL.Interface;
using static Dapper.SqlMapper;

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

        public async Task<ShoppingCart> AddToCard(int userId, Book book)
        {
            var shoppingCart = await _collection.FindAsync(x => x.UserId == userId);
            //a.Books.Add(book);
            if (shoppingCart == null)
            {
                return null;
            }
            shoppingCart.Current.GetEnumerator().Current.Books.Append(book);
            return shoppingCart.Current.GetEnumerator().Current;
        }

        public async Task<ShoppingCart> RemoveFromCart(int userId, Book book)
        {
            var shoppingCart = await _collection.FindAsync(x => x.UserId == userId);
            foreach (var i in shoppingCart.ToList())
            {
                _collection.DeleteOneAsync(i => i.Books.GetEnumerator().Current == book);
            }
            return shoppingCart.Current.GetEnumerator().Current;
        }

        public Task EmptyCart(int userId)
        {
            _collection.DeleteMany(x => x.UserId == userId);
            return Task.CompletedTask;
        }

        public async Task FinishPurchase(Purchase purchase)
        {
            await EmptyCart(purchase.UserId);
            await _purchaseRepository.SavePurchase(purchase);
        }
    }
}
