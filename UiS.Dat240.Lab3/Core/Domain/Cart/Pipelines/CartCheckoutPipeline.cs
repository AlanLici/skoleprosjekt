using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Services;
using System.Collections.Generic;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Core.Domain.Ordering;

namespace UiS.Dat240.Lab3.Core.Domain.Cart.Pipelines;

public class CartCheckout
{ //Inputen for Cartcheckout
    public record Request(
        Guid CartId,
        string Building,
        string RoomNumber,
        string LocationNotes,
        string CustomerName) : IRequest<Unit>;

    public class Handler : IRequestHandler<Request, Unit>
    {
        private readonly ShopContext _db;
        private readonly IOrderingService _orderingService;

        public Handler(ShopContext db, IOrderingService orderingService)
        { // Tar shopcontext og iorderingservice som dependencies
            _db = db;
            _orderingService = orderingService;
        }


        public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
        { 
            var cart = _db.ShoppingCart.Include(c => c.Items).SingleOrDefault(c => c.Id == request.CartId); // Henter Cart fra db basert på id
            if (cart == null)
            {
                cart = new ShoppingCart(request.CartId);
                _db.ShoppingCart.Add(cart);
            }

            List<OrderLineDto> orderLines = new List<OrderLineDto>(); // Lager en liste for orderline dataen

            foreach (var item in cart.Items) // legger inn data i liste
            {
                orderLines.Add(new OrderLineDto(item.Id, item.Name, item.Count, item.Price));
            }



            // Legger inn en order med orderingService. 
            var guid = await _orderingService.PlaceOrder(
                new Location(request.Building, request.RoomNumber, request.LocationNotes),
                request.CustomerName,
                orderLines.ToArray(),
                cancellationToken);

            // Lukker og Sletter Cart from session

            if (guid != null)
            {
                _db.ShoppingCart.Remove(cart);
                await _db.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
