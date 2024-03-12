using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
public record OfferShipperSet : BaseDomainEvent
{
    public OfferShipperSet(Guid orderId, string shipperName)
    {
        OrderId = orderId;
        ShipperName = shipperName;
    }

    public Guid OrderId { get; }
    public string ShipperName { get; }
}