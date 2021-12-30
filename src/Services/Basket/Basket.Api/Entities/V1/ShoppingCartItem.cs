namespace Basket.Api.Entities.V1;

public class ShoppingCartItem
{
    public int Quantity { get; set; } = 0;

    public string? Color { get; set; }

    public decimal Price { get; set; }

    public string? ProductId { get; set; }

    public string? ProductName { get; set; }
}
