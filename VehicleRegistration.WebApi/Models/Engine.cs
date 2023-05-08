using System.Text.Json.Serialization;

namespace VehicleRegistration.WebApi.Models;

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
    
    public EngineType? Type { get; set; }
    
    public int EngineTypeId { get; set; }
    
    public double HorsePower { get; set; }
    
    public double? Volume { get; set; }
    
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public Vehicle? Vehicle { get; set; }
    
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public int VehicleId { get; set; }
}