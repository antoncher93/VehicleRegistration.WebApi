using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;
using Xunit;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class RegistrationControllerTests
{
    [Fact]
    public async Task PostMethodReturnsRegistration()
    {
        using var sut = SutFactory.Create();
        var vehicle = Create.RandomVehicle();
        var owner = Create.RandomOwner();
        sut.SetupOwner(owner);
        sut.SetupVehicle(vehicle);
        sut.ClearRegistrations();
        var region = Values.RandomInt(1, 999);
        var regNumber = $"А000АА{region}";

        var result = await sut.RegistrationController.PostAsync(
            request: new AddRegistrationRequest()
            {
                Region = region,
                OwnerId = owner.Id,
                Vin = vehicle.VIN,
            });

        var expectedRegistration = new Registration(
            vehicle: vehicle,
            owner: owner,
            regNumber: regNumber);

        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(
                expectation: expectedRegistration,
                config: options => options
                    .Excluding(reg => reg.OwnerId)
                    .Excluding(reg => reg.VehicleId));
    }
}