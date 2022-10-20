using BookStore.Models.Models;

namespace BookStore.BL.Interfaces
{
    public interface IPurchaseService
    {
        public Task<Purchase?> SavePurchase(Purchase purchase);
        public Task<Guid> DeletePurchase(Purchase purchase);
        public Task<IEnumerable<Purchase>> GetAllPurchaseForUser(int userId);
    }
}
