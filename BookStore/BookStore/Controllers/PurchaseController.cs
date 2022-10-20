using BookStore.BL.Interfaces;
using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpPost(nameof(SavePurchase))]
        public async Task<IActionResult> SavePurchase(Purchase purchase)
        {
            return Ok(await _purchaseService.SavePurchase(purchase));
        }

        [HttpDelete(nameof(DeletePurchase))]
        public async Task<IActionResult> DeletePurchase(Purchase purchase)
        {
            return Ok(await _purchaseService.DeletePurchase(purchase));
        }

        [HttpGet(nameof(GetAllPurchaseForUser))]
        public async Task<IActionResult> GetAllPurchaseForUser(int userId)
        {
            return Ok(await _purchaseService.GetAllPurchaseForUser(userId));
        }
        
    }
}
