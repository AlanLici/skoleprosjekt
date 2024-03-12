using System.Linq;
using MediatR;
using UiS.Dat240.Lab3.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Exceptions;
using UiS.Dat240.Lab3.Core.Domain.Fulfillment.Events;
using Microsoft.EntityFrameworkCore;


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Handlers;

    public class OfferShipperSetHandler : INotificationHandler<OfferShipperSet>
    {
        private readonly ShopContext db;

       public OfferShipperSetHandler(ShopContext db)
        {
            this.db = db;
        }

        public async Task Handle(OfferShipperSet notification, CancellationToken cancellationToken)
        {

            var order = (await db.Orders
            .Where(o => o.Id == notification.OrderId)
            .FirstOrDefaultAsync(cancellationToken)) ?? throw new EntityNotFoundException();

            if (order.Status != Status.Placed)
            return;

            order.Status = Status.Shipped;
            await db.SaveChangesAsync(cancellationToken);

        }
    }