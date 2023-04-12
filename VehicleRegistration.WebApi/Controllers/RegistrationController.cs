using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/registration")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationRepository _registrations;

    public RegistrationController(
        IRegistrationRepository registrations)
    {
        _registrations = registrations;
    }

    public async Task<IActionResult> GetAsync(
        string regNumber)
    {
        var registration = await _registrations
            .FindRegistrationByRegNumber(regNumber);

        if (registration is null)
        {
            return this.NotFound();
        }

        return this.Ok(registration);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] AddRegistrationRequestModel request)
    {
        return this.BadRequest();
    }

    private Registration MapRegistration(
        AddRegistrationRequestModel request)
    {
        return new Registration();
    }
}