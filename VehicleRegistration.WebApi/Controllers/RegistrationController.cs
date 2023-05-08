using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.Tools;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/registration")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IOwnerRepository _ownerRepository;

    public RegistrationController(
        IRegistrationRepository registrationRepository, IVehicleRepository vehicleRepository, IOwnerRepository ownerRepository)
    {
        _registrationRepository = registrationRepository;
        _vehicleRepository = vehicleRepository;
        _ownerRepository = ownerRepository;
    }

    [HttpGet("{regNumber}/list")]
    public async Task<IActionResult> GetAsync(
        string regNumber)
    {
        var registrations = await _registrationRepository
            .FindRegistrationsByRegNumberAsync(regNumber);

        return this.Ok(registrations);
    }
    
    [HttpGet("byVehicleId/{vehicleId}")]
    public async Task<IActionResult> GetByVehicleIdAsync(
        int vehicleId)
    {
        var registrations = await _registrationRepository
            .FindRegistrationByVehicleId(vehicleId);

        return this.Ok(registrations);
    }
    
    [HttpGet("byOwnerId/{ownerId}")]
    public async Task<IActionResult> GetByOwnerIdAsync(
        int ownerId)
    {
        var registrations = await _registrationRepository
            .FindRegistrationByOwnerId(ownerId);

        return this.Ok(registrations);
    }

    [HttpDelete("{registrationId}/deactivate")]
    public async Task<IActionResult> DeactivateAsync(
        int registrationId)
    {
        var registration = await _registrationRepository.FindByIdAsync(registrationId);

        if (registration is null || !registration.IsActive)
            return this.BadRequest();

        await _registrationRepository.DeactivateAsync(registration);

        return this.Ok(registration);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] Registration registration)
    {
        registration.RegNumber = await GetNextRegNumberAsync(77);
        await _registrationRepository.AddAsync(registration);
        registration = await _registrationRepository.FindByIdAsync(registration.Id);
        return this.Ok(registration);
    }

    private async Task<string> GetNextRegNumberAsync(
        int region)
    {
        var busyNumbers = await _registrationRepository.GetBusyRegNumbersAsync();
        busyNumbers.Sort();
        var numberProvider = new RusRegNumberProvider();
        for (int i = 0; i < busyNumbers.Count; i++)
        {
            var expectedNumber = numberProvider.GetNextNumber() + region.ToString();
            if (busyNumbers[i] != expectedNumber)
            {
                return expectedNumber;
            }
        }

        return numberProvider.GetNextNumber() + region.ToString();
    }
}