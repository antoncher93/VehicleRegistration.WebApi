namespace VehicleRegistration.WebApi.Types;

public class Vehicle
{
    // конструктор для сериализации и EF
    public Vehicle()
    {
    }

    public Vehicle(
    string barnd, 
    string model, 
    string body, 
    string vin, 
    string 
    regNumber, 
    Owner owner, 
    Engine engine, 
    string color)
    {
        Barnd = barnd;
        Model = model;
        Body = body;
        VIN = vin;
        RegNumber = regNumber;
        Owner = owner;
        Engine = engine;
        Color = color;
    }
    
    public int Id { get; set; }
    
    public string Barnd { get; set; }
    
    public string Model { get; set; }
    
    public string Body { get; set; }
    
    public string VIN { get; set; }
    
    public string RegNumber { get; set; }
    
    public string Color { get; set; }
    
    public Engine Engine { get; set; }
    
    public int EngineId { get; set; }
    
    public Owner Owner { get; set; }
    
    public int OwnerId { get; set; }
}