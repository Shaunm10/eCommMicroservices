namespace AspnetRunBasics.Models;

public class BasketItemModel
{
    public int Quantity { get; set; } = 1;
    public string? Color { get; set; }
    public decimal Price { get; set; } = 0;
    public string? ProductId { get; set; }
    public string? ProductName { get; set; }
}
