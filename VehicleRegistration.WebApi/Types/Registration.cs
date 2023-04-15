namespace VehicleRegistration.WebApi.Types;

public class Registration
{
    public Registration()
    {
    }

    public Registration(Vehicle vehicle, Owner owner, string regNumber)
    {
        Vehicle = vehicle;
        Owner = owner;
        RegNumber = regNumber;
    }
    public int Id { get; set; }
    public string RegNumber { get; set; }
    public Vehicle Vehicle { get; set; }
    public Owner Owner { get; set; }
    public int OwnerId { get; set; }
    public int VehicleId { get; set; }
}