using System.Text.Json.Serialization;

namespace VehicleRegistration.WebApi.Types;

public class Owner
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string MiddleName { get; set; }
    
    [JsonIgnore]
    public List<Vehicle> Vehicles { get; set; }
}