using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/engine")]
public class EngineController : ControllerBase
{
    private readonly IEngineRepository _engines;

    public EngineController(
        IEngineRepository engines)
    {
        _engines = engines;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(
        int id)
    {
        var engine = await _engines.GetEngineAsync(id);

        if (engine is null)
        {
            return this.NotFound();
        }

        return this.Ok(engine);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] AddEngineRequestBody requestBody)
    {
        var engine = new Engine()
        {
            HorsePower = requestBody.HorsePower,
            Type = requestBody.EngineType,
            Volume = requestBody.Volume,
        };

        await _engines.AddEngineAsync(engine);

        return this.Ok(engine);
    }
}