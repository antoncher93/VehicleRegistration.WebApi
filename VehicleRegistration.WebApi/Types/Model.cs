using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Types;

public class Model
{
    public int Id { get; set; }
    
    public Brand Brand { get; set; } 
    
    [JsonIgnore]
    public int BrandId { get; set; }
    
    public string ModelName { get; set; }

    [JsonIgnore]
    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}