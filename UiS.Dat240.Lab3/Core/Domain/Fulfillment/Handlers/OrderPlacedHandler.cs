using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Handlers;
public class OrderPlacedHandler : INotificationHandler<OrderPlacedEvent>
{
    private readonly ShopContext _db;

    public OrderPlacedHandler(ShopContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    { //gettingg order lines for specifed order id
        var orderLines = await _db.Orders
            .Where(o => o.Id == notification.OrderId)
            .Include(o => o.OrderLines)
            .Select(o => o.OrderLines)
            .FirstOrDefaultAsync(cancellationToken);

        var offer = new Offer(notification.OrderId, orderLines!.Sum(x => x.TotalPrice));

        _db.Offers.Add(offer);
        await _db.SaveChangesAsync(cancellationToken);
    }
}