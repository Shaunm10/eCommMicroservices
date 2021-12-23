namespace Basket.Api.Entities;

public class ShoppingCart
{
    public string? UserName { get; set; }

    public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

    public ShoppingCart()
    {
    }

    public ShoppingCart(string userName)
    {
        this.UserName = userName;
    }

    public decimal TotalPrice
    {
        get
        {
            decimal totalPrice = 0;
            this.Items.ForEach(item => totalPrice += item.Price * item.Quantity);

            return totalPrice;
        }
    }
}
