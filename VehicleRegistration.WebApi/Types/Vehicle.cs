﻿using Newtonsoft.Json;

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
    Transmission transmission,
    string color)
    {
        Model = model;
        Body = body;
        VIN = vin;
        Engine = engine;
        Color = color;
        Transmission = transmission;
    }
    
    public int Id { get; set; }
    
    public Model Model { get; set; }
    
    [JsonIgnore]
    public int ModelId { get; set; }
    
    public Body Body { get; set; }
    
    [JsonIgnore]
    public int BodyId { get; set; }
    
    public string VIN { get; set; }
    
    public Transmission Transmission { get; set; }
    
    public string Color { get; set; }
    
    public Engine Engine { get; set; }
    
    [JsonIgnore]
    public int EngineId { get; set; }

    [JsonIgnore]
    public IEnumerable<Registration> Registrations { get; set; }
}