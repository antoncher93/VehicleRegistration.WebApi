namespace VehicleRegistration.WebApi.Types;

public class Vehicle
{
    // конструктор для сериализации и EF
    public Vehicle()
    {
    }

    public Vehicle(
    string vin,
    Model model,
    Body body,
    Engine engine, 
    string color)
    {
        Model = model;
        Body = body;
        VIN = vin;
        Engine = engine;
        Color = color;
    }
    
    public int Id { get; set; }
    
    public Model Model { get; set; }
    
    public int ModelId { get; set; }
    
    public Body Body { get; set; }
    
    public int BodyId { get; set; }
    
    public string VIN { get; set; }
    
    public string Color { get; set; }
    
    public Engine Engine { get; set; }
    
    public int EngineId { get; set; }
    
    public int OwnerId { get; set; }
}