using AspnetRunBasics.Models;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspnetRunBasics
{
    public class ProductModel : PageModel
    {
        //private readonly IProductRepository _productRepository;
        //private readonly ICartRepository _cartRepository;
        private readonly ICatalogService _catalogService;
        private readonly IBasketService _basketService;


        public ProductModel(ICatalogService catalogService, IBasketService basketService)
        {
            this._catalogService = catalogService ?? throw new ArgumentNullException(nameof(catalogService));
            this._basketService = basketService ?? throw new ArgumentNullException(nameof(basketService));
            //_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            //_cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }

        public IEnumerable<string> CategoryList { get; set; } = new List<string>();
        public IEnumerable<CatalogModel> ProductList { get; set; } = new List<CatalogModel>();


        [BindProperty(SupportsGet = true)]
        public string SelectedCategory { get; set; }

        public async Task<IActionResult> OnGetAsync(string categoryName)
        {
            // get all the products from the service
            var productList = await this._catalogService.GetCatalogAsync();

            // get a list of their categories.
            CategoryList = productList
                .Where(p => !string.IsNullOrWhiteSpace(p.Category))
                .Select(p => p.Category!)
                .Distinct()
                .OrderBy(x => x);


            // if there is a category specified
            if (!string.IsNullOrWhiteSpace(categoryName))
            {
                // filter on it.
                ProductList = productList
                    .Where(p => p.Category == categoryName);

                // and set it in memory.
                SelectedCategory = categoryName;
            }
            else
            {
                ProductList = productList;
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