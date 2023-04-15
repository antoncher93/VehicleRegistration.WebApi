using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Types;

public class Model
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public Brand Brand { get; set; } 
    
    public int BrandId { get; set; }
    
    public string ModelName { get; set; }

    public ICollection<Body> Bodies { get; set; } = new List<Body>();
    
    public ICollection<Engine> Engines { get; set; } = new List<Engine>();
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}