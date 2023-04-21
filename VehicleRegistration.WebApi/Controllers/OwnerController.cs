using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/owner")]
public class OwnerController : ControllerBase
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerController(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetByFullName(
        [FromQuery] GetOwnerByFullNameRequest request)
    {
        var owner = await _ownerRepository.FindByFullNameAsync(
            firstName: request.FirstName,
            lastName: request.LastName,
            middleName: request.MiddleName);

        if (owner is null)
        {
            return this.NotFound();
        }

        return this.Ok(owner);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] AddOwnerRequest request)
    {
        var owner = await _ownerRepository
            .FindByFullNameAsync(
                firstName: request.FirstName,
                lastName: request.LastName,
                middleName: request.MiddleName);

        if (owner != null)
        {
            return this.BadRequest();
        }
        
        owner = new Owner(
            firstName: request.FirstName,
            lastName: request.LastName,
            middleName: request.MiddleName);

        await _ownerRepository.AddAsync(owner);

        return this.Ok(owner);
    }
}