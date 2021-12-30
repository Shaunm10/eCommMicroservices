namespace Discount.Business.Entities.V1
{
    public class Discount
    {
        public int Id { get; set; }

        public string? ProductId { get; set; }

        public string? Description { get; set; }

        public decimal Amount { get; set; }
    }
}