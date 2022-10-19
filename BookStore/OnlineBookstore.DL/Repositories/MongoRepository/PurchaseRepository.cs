using BookStore.Models.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OnlineBookstore.DL.Interface;

namespace OnlineBookstore.DL.Repositories.MongoRepository
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IOptionsMonitor<MongoDbConfiguration> _settings;
        private readonly MongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly IMongoCollection<Purchase> _collection;
        public PurchaseRepository(IOptionsMonitor<MongoDbConfiguration> settings)
        {
            _settings = settings;
            _client = new MongoClient(_settings.CurrentValue.ConnectionString);
            _database = _client.GetDatabase(_settings.CurrentValue.DatabaseName);
            _collection = _database.GetCollection<Purchase>(_settings.CurrentValue.CollectionName);
        }
        public async Task<Purchase> SavePurchase(Purchase purchase)
        {
            await _collection.InsertOneAsync(purchase);
            return purchase;
        }

        public async Task<Guid> DeletePurchase(Purchase purchase)
        {
            var delete = await _collection.DeleteOneAsync(x => x.Id == purchase.Id);
            return purchase.Id;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchaseForUser(int userId)
        {
            var result = await _collection.FindAsync(x => x.UserId == userId);
            return result.ToList();
        }


    }
}
