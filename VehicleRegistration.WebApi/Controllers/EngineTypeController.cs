using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Controllers;

[Route("api/engines")]
[ApiController]
public class EngineTypeController : ControllerBase
{
    private readonly IEngineTypeRepository _engineTypeRepository;

    public EngineTypeController(IEngineTypeRepository engineTypeRepository)
    {
        _engineTypeRepository = engineTypeRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var engineTypes = await _engineTypeRepository.GetAllAsync();

        return this.Ok(engineTypes);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] PostEngineTypeRequest request)
    {
        var engineTypes = await _engineTypeRepository.GetAllAsync();

        if (engineTypes.Any(t => t.Name == request.EngineType))
        {
            return this.BadRequest("Такой тип двигателя уже существует");
        }

        var engineType = new EngineType()
        {
            Name = request.EngineType,
        };

        await _engineTypeRepository.AddAsync(engineType);

        return this.Ok(engineType);
    }
}