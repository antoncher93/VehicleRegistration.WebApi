using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

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
        [FromBody] EngineType engineType)
    {
        var engineTypes = await _engineTypeRepository.GetAllAsync();

        var isSameEngineTypeExists = engineTypes.Any(t => t.Name == engineType.Name);

        if (isSameEngineTypeExists)
        {
            return this.BadRequest("Такой тип двигателя уже существует");
        }

        await _engineTypeRepository.AddAsync(engineType);

        return this.Ok(engineType);
    }
}