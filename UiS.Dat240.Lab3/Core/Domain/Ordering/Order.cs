using System;
using System.Collections.Generic;
using UiS.Dat240.Lab3.Core.Domain.Ordering.Events;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering;

    public class Order : BaseEntity
    {
        public Order()
        {
        }
        public Order(string notes, Location location, Customer customer)
        {
            Customer = customer;
            Location = location;
            Notes = notes;
        }

        public Guid Id { get; protected set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public List<OrderLine> OrderLines { get; set; } = new List<OrderLine>();
        public Location Location { get; protected set; } = null!;
        public string Notes { get; set; }
        public Customer Customer { get; set; }
        public Status Status { get; set; }



    public void AddOrderLine(Status status, OrderLine orderLine)
    {
        if (status == Status.Placed)
        {
            OrderLines.Add(orderLine);
        }
    }

    public void CreateEvent()
    {
        var orderPlacedEvent = new OrderPlacedEvent(Id);
        Events.Add(orderPlacedEvent);
    }


}