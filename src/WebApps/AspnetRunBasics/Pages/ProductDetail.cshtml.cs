using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class ProductDetailModel : PageModel
    {
        //private readonly IProductRepository _productRepository;
        //private readonly ICartRepository _cartRepository;
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;

        public ProductDetailModel(ICatalogService catalogService, IBasketService basketService)
        {
            this._catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            this._basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
        }

        public CatalogModel Product { get; set; }

        [BindProperty]
        public string Color { get; set; }

        [BindProperty]
        public int Quantity { get; set; }

        public async Task<IActionResult> OnGetAsync(string productId)
        {
            if (productId == null)
            {
                return NotFound();
            }

            Product = await _catalogService.GetCatalogAsync(productId);
            if (Product == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(string productId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });

            var product = await this._catalogService.GetCatalogAsync(productId);

            var userName = "jts";
            var basket = await this._basketService.GetBasketAsync(userName);

            basket.Items.Add(new BasketItemModel
            {
                ProductId = productId,
                ProductName = product.Name,
                Price = product.Price.GetValueOrDefault(),
                Quantity = 1,
                Color = "Black"
            });

            var basketUpdated = await this._basketService.UpdateBasketAsync(basket);
            return RedirectToPage("Cart");
        }
    }
}