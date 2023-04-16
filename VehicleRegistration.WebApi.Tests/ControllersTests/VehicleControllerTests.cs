using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Tests.Extensions;
using VehicleRegistration.WebApi.Types;
using Xunit;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class VehicleControllerTests
{
    [Fact]
    public async Task FindMethodReturnsExistingVehicle()
    {
        using var sut = SutFactory.Create();
        var vin = Values.RandomString();
        var vehicle = Create.RandomVehicle(vin);
        sut.SetupVehicle(vehicle);

        var result = await sut.VehicleController.GetByVinAsync(vin);

        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(vehicle);
    }

    [Theory]
    [InlineData(Transmission.Automatic)]
    [InlineData(Transmission.Manual)]
    public async Task PostMethodReturnsVehicle(
        Transmission transmission)
    {
        using var sut = SutFactory.Create();

        var vin = Values.RandomString();
        var color = Values.RandomString();
        
        var model = Create.RandomModel();
        sut.SetupModels(model.AsList());

        var body = Create.RandomBody();
        sut.SetupBodies(body.AsList());

        var engine = Create.RandomEngine();

        var expectedVehicle = new Vehicle(
            vin: vin,
            model: model,
            body: body,
            engine: engine,
            transmission: transmission,
            color: color);

        var response = await sut.VehicleController.PostAsync(
            request: new AddVehicleRequest()
            {
                BodyId = body.Id,
                Engine = engine,
                ModelId = model.Id,
                Color = color,
                Transmission = (int)transmission,
                Vin = vin
            });

        response
            .As<OkObjectResult>()
            .Value.Should()
            .BeEquivalentTo(expectedVehicle,
                config: options => options
                    .Excluding(m => m.Id)
                    .Excluding(m => m.BodyId)
                    .Excluding(m => m.EngineId)
                    .Excluding(m => m.ModelId));
    }
}