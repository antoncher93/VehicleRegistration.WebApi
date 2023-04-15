using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/vehicle")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IModelRepository _modelRepository;
    private readonly IEngineRepository _engineRepository;
    private readonly IBodyRepository _bodyRepository;

    public VehicleController(
        IVehicleRepository vehicleRepository,
        IModelRepository modelRepository,
        IEngineRepository engineRepository,
        IBodyRepository bodyRepository)
    {
        _vehicleRepository = vehicleRepository;
        _modelRepository = modelRepository;
        _engineRepository = engineRepository;
        _bodyRepository = bodyRepository;
    }

    public async Task<IActionResult> GetByVinAsync(string vin)
    {
        var vehicle = await _vehicleRepository.FindByVinAsync(vin);
        
        if (vehicle is null)
            return this.NotFound();

        return this.Ok(vehicle);
    }

    public async Task<IActionResult> PostAsync(
        [FromBody] AddVehicleRequest request)
    {
        var model = await _modelRepository.GetByIdAsync(request.ModelId);
        if (model is null)
            return this.BadRequest();

        var engine = await _engineRepository.FindByIdAsync(request.EngineId);
        if (engine is null)
            return this.BadRequest();

        var body = await _bodyRepository.FindByIdAsync(request.BodyId);
        if (body is null)
            return this.BadRequest();

        var vehicle = new Vehicle(
            model: model,
            body: body,
            engine: engine,
            vin: request.Vin,
            color: request.Color);

        await _vehicleRepository.AddAsync(vehicle);

        return this.Ok(vehicle);
    }
}