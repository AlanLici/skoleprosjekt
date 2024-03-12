using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Core.Exceptions;
using UiS.Dat240.Lab3.Infrastructure.Data;


namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Pipelines;

public class GetOverview
{
    public record Request : IRequest<OrderOverviewDto[]>;

    public class Handler : IRequestHandler<Request, OrderOverviewDto[]>
    {
        private readonly ShopContext _db;
        public Handler(ShopContext db) => _db = db ?? throw new ArgumentNullException(nameof(db));

        public async Task<OrderOverviewDto[]> Handle(Request request, CancellationToken cancellationToken)
        {
            return await _db.Orders.Where(o => o.Status != Status.Declined)
                .Include(o => o.Customer)
                .Select(o => new OrderOverviewDto(o.Id, o.Customer.Name, o.Location.Building,o.Location.RoomNumber, o.OrderDate, o.Status))
                .ToArrayAsync(cancellationToken);
        }
    }
}