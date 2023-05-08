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
        await _engineRepository.AddEngineAsync(vehicle.Engine);
        await _vehicleRepository.AddAsync(vehicle);

        return this.Ok(vehicle);
    }
}