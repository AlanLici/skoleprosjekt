using System;
using UiS.Dat240.Lab3.SharedKernel;


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Events;


public record OrderPlacedEvent : BaseDomainEvent
{
    public Guid OrderId { get; }
    public OrderPlacedEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}