using System;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment;
public class Offer : BaseEntity
{
    private Offer()
    {
    }
    
    public Offer(Guid orderId, decimal amount)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        Reimbursement = new Reimbursement(amount);
    }

    public Guid Id { get; protected set; }
    public Guid OrderId { get; protected set; }

    private Shipper? _shipper;
    public Shipper? Shipper 
    { 
        get => _shipper;
        set
        {
            if(value == _shipper) return;
            if(value != null)
            {
                Events.Add(new OfferShipperSet(OrderId, value.Name));
            }
            _shipper = value;
        }
    }

    private Reimbursement _reimbursement;
    public Reimbursement Reimbursement
    {
        get => _reimbursement;
        set
        {
            if(value == null || value.Amount <= 0)
                return;
            _reimbursement = value;
        }
    }
}