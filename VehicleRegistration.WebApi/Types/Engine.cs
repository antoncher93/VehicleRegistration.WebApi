using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Types;

public class Engine
{
    public Engine()
    {
    }
    
    public Engine(
        string number,
        EngineType type,
        double horsePower,
        double? volume)
    {
        Number = number;
        Type = type;
        HorsePower = horsePower;
        Volume = volume;
    }
    public int Id { get; set; }
    
    public string Number { get; set; }
    
    public EngineType Type { get; set; }
    
    [JsonIgnore]
    public int EngineTypeId { get; set; }
    
    public double HorsePower { get; set; }
    
    public double? Volume { get; set; }
    
    [JsonIgnore]
    public Vehicle Vehicle { get; set; }
    
    [JsonIgnore]
    public int VehicleId { get; set; }
}