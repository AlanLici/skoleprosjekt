using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing;
public class Payment : BaseEntity
{
    public Payment(decimal amount)
    {
        Amount = amount;
    }

    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; protected set; }
}