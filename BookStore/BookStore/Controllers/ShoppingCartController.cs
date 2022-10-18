using BookStore.Models.Models;
using Microsoft.AspNetCore.Mvc;
using OnlineBookstore.DL.Interface;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartService _cartService;

        public ShoppingCartController(IShoppingCartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet(nameof(GetContent))]
        public async Task<IActionResult> GetContent(int userId)
        {
            return Ok(await _cartService.GetContent(userId));
        }

        [HttpPost(nameof(AddToCart))]
        public async Task<IActionResult> AddToCart(ShoppingCart cart)
        {
            return Ok(await _cartService.AddToCard(cart));
        }

        [HttpDelete(nameof(RemoveFromCart))]
        public async Task<IActionResult> RemoveFromCart(ShoppingCart cart)
        {
            return Ok(await _cartService.RemoveFromCart(cart));
        }

        [HttpPost(nameof(EmptyCart))]
        public Task<IActionResult> EmptyCart(int userId)
        {
            return Task.FromResult<IActionResult>(Ok(_cartService.EmptyCart(userId)));
        }

        [HttpPost(nameof(FinishedPurchase))]
        public async Task<IActionResult> FinishedPurchase(Purchase purchase)
        {
            return Ok( _cartService.FinishPurchase(purchase));
        }
    }
}
