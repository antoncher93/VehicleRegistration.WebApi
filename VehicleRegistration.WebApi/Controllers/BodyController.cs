using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

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
        [FromBody] AddBodyRequest request)
    {
        var bodies = await _bodies.GetBodiesAsync();

        if (bodies.Any(b => b.Name == request.Name))
        {
            return this.BadRequest("Такой тип кузова уже есть в базе");
        }

        var body = new Body()
        {
            Name = request.Name,
        };

        await _bodies.AddBodyAsync(body);
        
        return this.Ok(body);
    }
}