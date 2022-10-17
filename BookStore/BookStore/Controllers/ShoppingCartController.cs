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
        public async Task<IActionResult> AddToCart(int userId, Book book)
        {
            return Ok(await _cartService.AddToCard(userId, book));
        }

        [HttpDelete(nameof(RemoveFromCart))]
        public async Task<IActionResult> RemoveFromCart(int userId, Book book)
        {
            return Ok(await _cartService.RemoveFromCart(userId, book));
        }

        [HttpPost(nameof(EmptyCart))]
        public Task<IActionResult> EmptyCart(int userId)
        {
            return Task.FromResult<IActionResult>(Ok(_cartService.EmptyCart(userId)));
        }

        [HttpPost(nameof(FinishedPurchase))]
        public Task<IActionResult> FinishedPurchase(Purchase purchase)
        {
            return Task.FromResult<IActionResult>(Ok(_cartService.FinishPurchase(purchase)));
        }
    }
}
