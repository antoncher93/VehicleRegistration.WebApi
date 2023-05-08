namespace VehicleRegistration.WebApi.Models;

public class ModelEngine
{
    public int Id { get; set; }
    public int EngineId { get; set; }
    public int ModelId { get; set; }
    public Engine Engine { get; set; }
    public Model Model { get; set; }
}