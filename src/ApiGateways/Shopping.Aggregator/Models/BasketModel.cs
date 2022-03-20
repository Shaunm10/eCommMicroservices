using System;

namespace Shopping.Aggregator.Models;

public class BasketModel
{
    public string? UserName { get; set; }

    public List<BasketItemExtendedModel> Items { get; set; } = new List<BasketItemExtendedModel>();

    public decimal? TotalPrice { get; set; }

    // public string? Category { get; set; }

    //public string? Summary { get; set; }

    //public string? Description { get; set; }

    // public string? ImageFile { get; set; }
}