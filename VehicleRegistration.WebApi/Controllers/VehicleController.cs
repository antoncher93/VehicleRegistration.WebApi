using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.RequestModels;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/vehicle")]
public class VehicleController : ControllerBase
{
    public async Task<IActionResult> FindByRegNumber(
        [FromBody] AddVehicleRequest request)
    {
        return this.Ok();
    }
}