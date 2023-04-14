using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Types;
using Xunit;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class BodyControllerTests
{
    [Fact]
    public async Task ReturnsAllAvailableBodies()
    {
        using var sut = SutFactory.Create();

        var bodies = Values.ListOf(Create.RandomBody);

        sut.SetupBodies(bodies);

        var response = await sut.BodyController.GetAsync();

        response
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(bodies);
    }

    [Fact]
    public async Task PostMethodAReturnsBody()
    {
        using var sut = SutFactory.Create();

        var body = Create.RandomBody();

        await sut.BodyController.PostAsync(body);

        var result = await sut.BodyController.GetAsync();

        result
            .As<OkObjectResult>()
            .Value
            .As<List<Body>>()
            .Should()
            .Contain(b => b.Name == body.Name);
    }
}