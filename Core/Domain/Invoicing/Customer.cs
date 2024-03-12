using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Invoicing;
public class Customer : BaseEntity
{
    public string Name { get; set;} 

    public Guid Id { get; set; }

    public Customer()
    {
    }
     public Customer(string name)
    {
        Name = name;
    }
}