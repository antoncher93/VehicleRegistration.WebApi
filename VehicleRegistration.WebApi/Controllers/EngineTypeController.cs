using Microsoft.AspNetCore.Mvc;
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

    public async Task<IActionResult> GetAsync()
    {
        var engineTypes = await _engineTypeRepository.GetAllAsync();

        return this.Ok(engineTypes);
    }
}