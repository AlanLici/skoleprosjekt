using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing;

public class Invoice : BaseEntity
{
    public Invoice() { }
    public Invoice(Guid orderId, Customer customer, Address address, decimal amount)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Address = address;
        Customer = customer;
        Amount = amount;
        Status = Status.New;
    }

    public Guid Id { get; protected set; }
    public Customer Customer { get; set; } = null!;
    public Address Address { get; set; } = null!;
    public decimal Amount { get; protected set; }
    public Guid OrderId { get; protected set; }
    public Status Status { get; protected set; }
}