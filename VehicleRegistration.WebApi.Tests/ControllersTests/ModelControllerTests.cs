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
            .BeEquivalentTo(models);
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
    public async Task PostMethodReturnsModel()
    {
        // arrange
        using var sut = SutFactory.Create();
        var brand = Create.RandomBrand();
        sut.SetupBrand(brand);
        
        var model = Create.RandomModel(brand);

        // action
        var result = await sut.ModelController.PostAsync(
            request: new AddModelRequest()
            {
                BrandId = brand.Id,
                Name = model.ModelName,
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