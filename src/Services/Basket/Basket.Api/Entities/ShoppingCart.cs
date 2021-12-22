namespace Basket.Api.Entities;

public class ShoppingCart
{
    public string? UserName { get; set; }

    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();
}
