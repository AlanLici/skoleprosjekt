using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Exceptions;
using UiS.Dat240.Lab3.Infrastructure.Data;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment.Pipelines;

public class AssignShipper
{
    public record Request(Guid OrderId, string ShipperName) : IRequest<bool>;

    public class Handler : IRequestHandler<Request, bool>
    {
        private readonly ShopContext _db;
        public Handler(ShopContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<bool> Handle(Request request, CancellationToken cancellationToken)
        {
            var offer = await _db.Offers
                .Where(o => o.OrderId == request.OrderId)
                .Include(o => o.Shipper)
                .FirstOrDefaultAsync(cancellationToken);

            if(offer == null)
                throw new EntityNotFoundException($"No offer made for order with ${request.OrderId}");
            
            if(offer.Shipper != null) 
            return false;

            var shipper = new Shipper(request.ShipperName);
            _db.Shipper.Add(shipper);

            offer.Shipper = shipper;
            return await _db.SaveChangesAsync(cancellationToken) >= 1;
        }
    }
}
