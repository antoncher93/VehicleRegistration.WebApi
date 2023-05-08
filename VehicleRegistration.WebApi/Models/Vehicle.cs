using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Models;

public class Vehicle
{
    public int Id { get; set; }
    
    public Model? Model { get; set; }
    
    public int ModelId { get; set; }
    
    public Body? Body { get; set; }
    
    public int BodyId { get; set; }
    
    public string VIN { get; set; }
    
    public Transmission Transmission { get; set; }
    
    public string Color { get; set; }
    
    public Engine? Engine { get; set; }
    
    public int EngineId { get; set; }

    [JsonIgnore]
    public IEnumerable<Registration>? Registrations { get; set; }
}