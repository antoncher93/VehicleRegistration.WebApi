using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Repositories;
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
        [FromBody] Body body)
    {
        try
        {
            await _bodies.AddBodyAsync(body);
        }
        catch (DbUpdateException)
        {
            return this.BadRequest();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
        return this.Ok(body);
    }
}