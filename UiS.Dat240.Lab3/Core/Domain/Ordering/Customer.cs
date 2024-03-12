using System;

namespace UiS.Dat240.Lab3.Core.Domain.Ordering;


public record Customer
{
    public string Name { get; set;} 

    public Guid Id { get; set; }

    public Customer()
    {
    }
     public Customer(string name)
    {
        Name = name;
        Id = Guid.NewGuid();
    }
}