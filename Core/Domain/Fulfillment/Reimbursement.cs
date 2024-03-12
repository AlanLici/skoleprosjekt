using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment;
public class Reimbursement : BaseEntity
{
    public Reimbursement(decimal amount)
    {
        Amount = amount;
        Id = new Guid();
    }

    public Guid Id { get; protected set; }
    public decimal Amount { get; protected set; }
    public Shipper? Shipper { get; set; }
    public Guid? InvoiceId { get; set; }
}