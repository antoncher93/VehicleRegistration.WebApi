namespace VehicleRegistration.WebApi.Models;

public class Registration
{
    public int Id { get; set; }
    public string? RegNumber { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Owner? Owner { get; set; }
    public int OwnerId { get; set; }
    public int VehicleId { get; set; }
    
    public bool IsActive { get; set; }
}