using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

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
    public async Task<IActionResult> GetAsync(
        int passportSeries,
        int passportNumber)
    {
        var owner = await _ownerRepository.FindByPassportDataAsync(passportSeries, passportNumber);

        if (owner is null)
        {
            return this.NotFound();
        }

        return this.Ok(owner);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] Owner owner)
    {
        // Ищем собственника с такими же ФИО
        var sameOwner = await _ownerRepository
            .FindByPassportDataAsync(owner.PassportSeries, owner.PassportNumber);

        // если такой собственник уже есть
        if (sameOwner != null)
        {
            // возвращаем ошибку
            return this.BadRequest("Такой собственник уже есть в базе");
        }
        
        await _ownerRepository.AddAsync(owner);

        return this.Ok(owner);
    }
}