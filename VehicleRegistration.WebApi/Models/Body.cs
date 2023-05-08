using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Models;

public class Body
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public ICollection<Vehicle>? Vehicles { get; set; }
}