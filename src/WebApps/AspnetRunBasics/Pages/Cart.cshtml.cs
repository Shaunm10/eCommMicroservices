using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class CartModel : PageModel
    {
        //private readonly ICartRepository _cartRepository;
        private readonly IBasketService _basketService;

        public CartModel(IBasketService basketService)
        {
            this._basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            //_cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public BasketModel Cart { get; set; } = new BasketModel();

        public async Task<IActionResult> OnGetAsync()
        {
            var userName = "jts";
            Cart = await this._basketService.GetBasketAsync(userName);

            return Page();
        }

        public async Task<IActionResult> OnPostRemoveToCartAsync(string productId)
        {
            var userName = "jts";
            var basket = await this._basketService.GetBasketAsync(userName);

            // get the basket
            var item = basket.Items.FirstOrDefault(x => x.ProductId == productId);

            if (item != null)
            {
                basket.Items.Remove(item);
            }

            // update the DB
            var basketUpdated = await this._basketService.UpdateBasketAsync(basket);

            return RedirectToPage();
        }
    }
}