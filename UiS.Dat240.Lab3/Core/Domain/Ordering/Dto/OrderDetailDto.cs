using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http.Features;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment;
using UiS.Dat240.Lab3.Core.Domain.Invoicing;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;

public record OrderDetailsDto
{
    public OrderDetailsDto(Order order, Invoice invoice, Offer offer)
    {
        Id = order.Id;

        CustomerName = order.Customer.Name;
        RoomNumber = order.Location.RoomNumber;
        Building = order.Location.Building; 
        Notes = order.Location.Notes; 
        OrderDate = order.OrderDate;
        Status = order.Status;

        ShipperName = offer?.Shipper?.Name;

        InvoiceAmount = invoice?.Amount;
        InvoiceStatus = invoice?.Status != null ? invoice.Status.ToString() : null; // brute
        
        Items = order.OrderLines.ToList() ?? new List<OrderLine>();   
    }
    public Guid Id { get; init; }
    public string CustomerName { get; protected set; } 
    public string RoomNumber { get; set; }
    public string Building { get; set; }
    public string Notes { get; set; }
    public DateTime OrderDate { get; protected set; }
    public Status Status { get; protected set; }
    public string? ShipperName { get; protected set; }
    public List<OrderLine> Items { get; protected set; }
    public decimal? InvoiceAmount { get; protected set; }
    public string InvoiceStatus { get; protected set; }

    
}