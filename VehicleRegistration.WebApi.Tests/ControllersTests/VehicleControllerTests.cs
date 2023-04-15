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

    [Fact]
    public async Task PostMethodReturnsVehicle()
    {
        using var sut = SutFactory.Create();

        var vin = Values.RandomString();
        var color = Values.RandomString();
        
        var model = Create.RandomModel();
        sut.SetupModels(model.AsList());

        var body = Create.RandomBody();
        sut.SetupBodies(body.AsList());

        var engine = Create.RandomEngine(models: model.AsList());
        sut.SetupEngines(engine.AsList());

        var expectedVehicle = new Vehicle(
            vin: vin,
            model: model,
            body: body,
            engine: engine,
            color: color);

        var response = await sut.VehicleController.PostAsync(
            request: new AddVehicleRequest()
            {
                BodyId = body.Id,
                EngineId = engine.Id,
                ModelId = model.Id,
                Color = color,
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