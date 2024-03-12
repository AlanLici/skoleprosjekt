using System;
using Microsoft.AspNetCore.Razor.Language;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering;

public class OrderLine
{
    public Guid Id { get; set; }

    public Guid Item { get; set;}
    
    public decimal Price { get; set; }

    public int Amount { get; set; }

    public decimal TotalPrice => Price * Amount;

    public OrderLine()
    {
    }

    public OrderLine(Guid item, decimal price, int amount)
    {
        Item = item;
        Price = price;
        Amount = amount;
    }

}