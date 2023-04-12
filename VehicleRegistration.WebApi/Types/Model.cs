using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Types;

public class Model
{
    public int Id { get; set; }
    
    [JsonIgnore]
    public Brand Brand { get; set; } 
    
    public int BrandId { get; set; }
    
    public string ModelName { get; set; }

    public List<Body> Bodies { get; set; } = new List<Body>();
    
    public List<Engine> Engines { get; set; } = new List<Engine>();
}