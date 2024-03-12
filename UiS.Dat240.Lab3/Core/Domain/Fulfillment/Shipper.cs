using System;
using UiS.Dat240.Lab3.SharedKernel;

namespace UiS.Dat240.Lab3.Core.Domain.Fulfillment;

public class Shipper : BaseEntity
{
    public Shipper(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }

    public Guid Id { get; protected set; }

    private string _name = null!;
    public string Name
    { 
        get => _name; 
        set
        {
            if(!string.IsNullOrWhiteSpace(value))
                _name = value;
        }
    }
}