using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Dto;
using UiS.Dat240.Lab3.Infrastructure.Data;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering.Services;

public interface IOrderingService
{
       Task<Guid> PlaceOrder(Location location, string customerName, OrderLineDto[] orderLines, CancellationToken CancellationToken);
       
}

public class OrderingService : IOrderingService
{
    private readonly ShopContext Db;

    public OrderingService(ShopContext db)
    {
        Db = db;
    }

    public async Task<Guid> PlaceOrder(Location location, string customerName, OrderLineDto[] orderLines, CancellationToken CancellationToken)
    {
        var customer = Db.Customers.FirstOrDefault(c => c.Name == customerName);
        if (customer == null)
        {
            customer = new Customer(customerName);
            Db.Customers.Add(customer);
        }

        var Order = new Order("", location, new Customer(customerName));
        Db.Orders.Add(Order);


        foreach (var orderLineDto in orderLines)
        {
            var orderLine = new OrderLine(orderLineDto.FoodItemId, orderLineDto.Price, orderLineDto.Amount);
            Order.OrderLines.Add(orderLine);
        }

        Order.Status = Status.Placed;
        Db.Orders.Add(Order);
        Order.CreateEvent();
        await Db.SaveChangesAsync(CancellationToken);
        return Order.Id;
    }
}