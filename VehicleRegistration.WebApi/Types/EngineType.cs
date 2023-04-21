using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Types;

public class EngineType
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public ICollection<Engine>? Engines { get; set; }
}