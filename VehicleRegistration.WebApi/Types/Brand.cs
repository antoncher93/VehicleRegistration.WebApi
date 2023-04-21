using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Types;

public class Brand
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    [JsonIgnore]
    public List<Model> Models { get; set; } = new List<Model>();
}