namespace UiS.Dat240.Lab3.Core.Domain.Invoicing;

public class Address
{
    public Address(string building, string roomNumber, string notes)
    {
        Building = building;
        RoomNumber = roomNumber;
        Notes = notes;
    }

    public string Building { get; protected set; }
    public string RoomNumber { get; protected set; }
    public string Notes { get; protected set; }

}