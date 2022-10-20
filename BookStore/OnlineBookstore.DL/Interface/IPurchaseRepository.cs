using BookStore.Models.Models;

namespace OnlineBookstore.DL.Interface
{
    public interface IPurchaseRepository
    {
        Task<Purchase> SavePurchase(Purchase purchase);

        Task<Guid> DeletePurchase(Purchase purchase);

        Task<IEnumerable<Purchase>> GetAllPurchaseForUser(int userId);


    }
}
