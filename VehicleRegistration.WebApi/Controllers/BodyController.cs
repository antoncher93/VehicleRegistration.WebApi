using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/bodies")]
public class BodyController : ControllerBase
{
    private readonly IBodyRepository _bodies;

    public BodyController(
        IBodyRepository bodies)
    {
        _bodies = bodies;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var bodies = await _bodies.GetBodiesAsync();
        return this.Ok(bodies);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] Body body)
    {
        var bodies = await _bodies.GetBodiesAsync();

        if (bodies.Any(b => b.Name == body.Name))
        {
            return this.BadRequest("Такой тип кузова уже есть в базе");
        }

        await _bodies.AddBodyAsync(body);
        
        return this.Ok(body);
    }
}