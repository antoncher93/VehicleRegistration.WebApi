using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Types;

public class Engine
{
    public int Id { get; set; }
    
    public EngineType Type { get; set; }
    
    public double HorsePower { get; set; }
    
    public double? Volume { get; set; }
    
    [JsonIgnore]
    public List<Model> Models { get; set; } = new List<Model>();
}