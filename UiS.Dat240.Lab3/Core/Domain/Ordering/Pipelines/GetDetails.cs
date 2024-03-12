using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Core.Exceptions;
using UiS.Dat240.Lab3.Infrastructure.Data;
namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Pipelines;

public class GetDetails
{
    public record Request(Guid OrderId) : IRequest<OrderDetailsDto>;

    public class Handler : IRequestHandler<Request, OrderDetailsDto>
    {
        private readonly ShopContext _db;

        public Handler(ShopContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<OrderDetailsDto> Handle(Request request, CancellationToken cancellationToken)
        {
            var order = await _db.Orders
                .Include(o => o.Customer)
                .Include(o => o.Location)
                .Include(o => o.OrderLines)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken) ?? throw new EntityNotFoundException();

            var invoice = await _db.Invoices
                .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);

            var offer = await _db.Offers
                .Include(x => x.Reimbursement)
                .Include(x => x.Shipper)
                .FirstOrDefaultAsync(x => x.OrderId == request.OrderId, cancellationToken);

            return new OrderDetailsDto(order, invoice, offer);
        }
    }
}