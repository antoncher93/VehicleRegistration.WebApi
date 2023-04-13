using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Tests.Extensions;
using VehicleRegistration.WebApi.Types;
using Xunit;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class EngineControllerTests
{
    public static IEnumerable<object[]> EngineTestData => TestData.GetData();
    
    [Fact]
    public async Task ReturnsExistingEngine()
    {
        var engine = EntityValues.RandomEngine();
        using var sut = SutFactory.Create();
        sut.SetupEngines(engine.AsList());

        var result = await sut.EngineController.GetAsync(engine.Id);

        result.As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(engine);
    }

    [Theory]
    [MemberData(nameof(EngineTestData))]
    public async Task PostMethodReturnsEngine(
        EngineData testCase)
    {
        using var sut = SutFactory.Create();
        var result = await sut.EngineController.PostAsync(
            requestBody: new AddEngineRequestBody()
            {
                EngineType = testCase.EngineType,
                HorsePower = testCase.HorsePower,
                Volume = testCase.Volume,
            });

        var expectedEngine = new Engine()
        {
            HorsePower = testCase.HorsePower,
            Type = testCase.EngineType,
            Volume = testCase.Volume,
        };

        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(expectedEngine,
                config: options => options
                    .Excluding(e => e.Id));
    }

    public static class TestData
    {
        public static IEnumerable<object[]> GetData()
        {
            yield return new object[]
            {
                new EngineData(
                    engineType: EngineType.Diesel,
                    horsePower: 175.0,
                    volume: 2.5),
            };

            yield return new object[]
            {
                new EngineData(
                    engineType: EngineType.Electrical,
                    horsePower: 240,
                    volume: null),
            };
        }
    }
    
    public class EngineData
    {
        public EngineData(EngineType engineType, double horsePower, double? volume)
        {
            EngineType = engineType;
            HorsePower = horsePower;
            Volume = volume;
        }
        public EngineType EngineType { get; }
        public double HorsePower { get; }
        public double? Volume { get; }
    }
}