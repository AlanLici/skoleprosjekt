using System;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Ordering;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.Infrastructure.Data;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing.Handlers;

public class OrderPlacedHandler : INotificationHandler<OrderPlacedEvent>
{
    private readonly ShopContext _db;

    public OrderPlacedHandler(ShopContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

    public async Task Handle(OrderPlacedEvent notification, CancellationToken cancellationToken)
    {
        Order order = (await _db.Orders
            .Where(o => o.Id == notification.OrderId)
            .Include(o => o.Customer)
            .FirstOrDefaultAsync(cancellationToken))!;

        var customer = new Customer(order.Customer.Name);
        
        var address = new Address(order.Location.Building, order.Location.RoomNumber, order.Location.Notes);
        
        var invoice = new Invoice(notification.OrderId, customer, address, order.OrderLines.Sum(ol => ol.TotalPrice));

        _db.Invoices.Add(invoice);
        await _db.SaveChangesAsync(cancellationToken);
    }
}