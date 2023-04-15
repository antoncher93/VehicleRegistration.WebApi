using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;
using Xunit;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class ModelControllerTests
{
    [Fact]
    public async Task ReturnsExistingModels()
    {
        using var sut = SutFactory.Create();
        var brand = Create.RandomBrand();
        sut.SetupBrand(brand);
        var models = Values.ListOf(() => Create.RandomModel(brand));
        sut.SetupModels(models);
        var result = await sut.ModelController.GetModelsAsync(brand.Name);
        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(brand.Models);
    }

    [Fact]
    public async Task ReturnsNotFoundIfBrandNoBrand()
    {
        using var sut = SutFactory.Create();
        var brandName = Values.RandomString();
        var result = await sut.ModelController.GetModelsAsync(brandName);
        result
            .Should()
            .BeOfType<NotFoundResult>();
    }

    [Fact]
    public async Task GetModelDetailsReturnsNotFoundIfDoesNotFoundModel()
    {
        using var sut = SutFactory.Create();
        var brand = Create.RandomBrand();
        sut.SetupBrand(brand);
        var models = Values.ListOf(() => Create.RandomModel(brand));
        sut.SetupModels(models);
        var result = await sut.ModelController.GetModelDetailsAsync(
            brandName: brand.Name,
            modelName: Values.RandomString());
        result
            .Should()
            .BeOfType<NotFoundResult>();
    }
    
    [Fact]
    public async Task GetModelDetailsReturnsModel()
    {
        using var sut = SutFactory.Create();
        var brand = Create.RandomBrand();
        sut.SetupBrand(brand);
        var models = Values.ListOf(() => Create.RandomModel(brand));
        sut.SetupModels(models);

        var randomModel = models
            .OrderBy(_ => Values.RandomInt())
            .First();
        
        var result = await sut.ModelController.GetModelDetailsAsync(
            brandName: randomModel.Brand.Name,
            modelName: randomModel.ModelName);
        
        result.As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(randomModel);
    }

    [Fact]
    public async Task PostMethodReturnsModel()
    {
        // arrange
        using var sut = SutFactory.Create();
        var brand = Create.RandomBrand();
        sut.SetupBrand(brand);

        var bodies = Values.ListOf(Create.RandomBody);
        sut.SetupBodies(bodies);

        var engines = Values.ListOf(() => Create.RandomEngine());
        sut.SetupEngines(engines);
        
        var model = Create.RandomModel(brand);
        model.Bodies = bodies;
        model.Engines = engines;

        // action
        var result = await sut.ModelController.PostAsync(
            request: new AddModelRequest()
            {
                BrandId = brand.Id,
                Name = model.ModelName,
                BodyIds = bodies.Select(body => body.Id).ToList(),
                EngineIds = engines.Select(engine => engine.Id).ToList(),
            });

        // assert
        result
            .As<OkObjectResult>()
            .Value
            .As<Model>()
            .Should()
            .BeEquivalentTo(
                expectation: model,
                config: options => options.Excluding(m => m.Id));
    }
}