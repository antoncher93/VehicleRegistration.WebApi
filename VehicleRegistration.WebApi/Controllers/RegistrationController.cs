﻿using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Infrastructure;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/registration")]
public class RegistrationController : ControllerBase
{
    private readonly IRegistrationRepository _registrationRepository;
    private readonly IVehicleRepository _vehicleRepository;
    private readonly IOwnerRepository _ownerRepository;

    public RegistrationController(
        IRegistrationRepository registrationRepository,
        IVehicleRepository vehicleRepository,
        IOwnerRepository ownerRepository)
    {
        _registrationRepository = registrationRepository;
        _vehicleRepository = vehicleRepository;
        _ownerRepository = ownerRepository;
    }

    public async Task<IActionResult> GetAsync(
        string regNumber)
    {
        var registration = await _registrationRepository
            .FindRegistrationByRegNumber(regNumber);

        if (registration is null)
        {
            return this.NotFound();
        }

        return this.Ok(registration);
    }

    [HttpPatch]
    public async Task<IActionResult> DisableAsync(
        [FromBody] int registrationId)
    {
        var registration = await _registrationRepository.FindByIdAsync(registrationId);

        if (registration is null || !registration.IsActive)
            return this.BadRequest();

        registration.IsActive = false;

        await _registrationRepository.UpdateAsync(registration);

        return this.Ok(registration);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] AddRegistrationRequest request)
    {
        var vehicle = await _vehicleRepository.FindByVinAsync(request.Vin);
        
        if(vehicle is null)
            return this.BadRequest("Не удалось найти ТС с указанным VIN");

        var owner = await _ownerRepository.FindByIdAsync(request.OwnerId);
        if (owner is null)
            return this.BadRequest("Не удалось найти указанного владельца");

        var regNumber = await GetNextRegNumberAsync(request.Region);

        var registration = new Registration(
            owner: owner,
            vehicle: vehicle,
            regNumber: regNumber,
            isActive: true);

        await _registrationRepository.AddAsync(registration);

        return this.Ok(registration);
    }

    private async Task<string> GetNextRegNumberAsync(
        int region)
    {
        var busyNumbers = await _registrationRepository.GetAllRegNumbersAsync();
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