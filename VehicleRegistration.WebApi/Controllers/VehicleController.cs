using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/vehicle")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IEngineRepository _engineRepository;

    public VehicleController(IVehicleRepository vehicleRepository, IEngineRepository engineRepository)
    {
        _vehicleRepository = vehicleRepository;
        _engineRepository = engineRepository;
    }

    [HttpGet("{vin}")]
    public async Task<IActionResult> GetByVinAsync(string vin)
    {
        var vehicle = await _vehicleRepository
            .FindByVinAsync(vin);
        
        if (vehicle is null)
            return this.NotFound();

        return this.Ok(vehicle);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] Vehicle vehicle)
    {
        var engine = await _engineRepository.FindByNumberAsync(vehicle.Engine.Number);

        if (engine != null)
        {
            return BadRequest("Автомобиль с указанным номером двигателя уже есть в базе");
        }
        
        await _engineRepository.AddEngineAsync(vehicle.Engine);
        await _vehicleRepository.AddAsync(vehicle);

        return this.Ok(vehicle);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateAsync(
        [FromBody] Vehicle vehicle)
    {
        var existingVehicle = await _vehicleRepository.FindByVinAsync(vehicle.VIN);

        if (existingVehicle is null)
        {
            return BadRequest("Отсутствует автомобиль с указанным VIN");
        }

        var engine = await _engineRepository.FindByNumberAsync(vehicle.Engine.Number);

        if (engine is null)
        {
            engine = new Engine()
            {
                Number = vehicle.Engine.Number,
                EngineTypeId = vehicle.Engine.EngineTypeId,
                HorsePower = vehicle.Engine.HorsePower,
                Volume = vehicle.Engine.Volume,
            };

            await _engineRepository.AddEngineAsync(engine);
        }

        existingVehicle.EngineId = engine.Id;
        existingVehicle.ModelId = vehicle.ModelId;
        existingVehicle.Color = vehicle.Color;
        existingVehicle.BodyId = vehicle.BodyId;
        existingVehicle.Transmission = vehicle.Transmission;

        await _vehicleRepository.UpdateAsync(existingVehicle);

        return Ok(existingVehicle);
    }
}