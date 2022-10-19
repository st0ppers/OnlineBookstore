using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using OnlineBookstore.DL.Interface;

namespace BookStore.BL.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public Task<Purchase?> SavePurchase(Purchase purchase)
        {
            return _purchaseRepository.SavePurchase(purchase);
        }

        public Task<Guid> DeletePurchase(Purchase purchase)
        {
            return _purchaseRepository.DeletePurchase(purchase);
        }

        public Task<IEnumerable<Purchase>> GetAllPurchaseForUser(int userId)
        {
            return _purchaseRepository.GetAllPurchaseForUser(userId);
        }
    }
}
