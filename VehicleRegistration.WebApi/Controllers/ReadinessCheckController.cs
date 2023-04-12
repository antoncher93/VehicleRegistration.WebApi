using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Responses;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/readinesscheck")]
public class ReadinessCheckController : ControllerBase
{
    public ReadinessCheckResponse Get()
    {
        return ReadinessCheckResponse.FromOk();
    }
}