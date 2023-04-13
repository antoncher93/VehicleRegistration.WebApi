using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class ModelControllerTests
{
    [Fact]
    public async Task ReturnsExistingModels()
    {
        using var sut = SutFactory.Create();
        var brand = EntityValues.RandomBrand();
        sut.SetupBrand(brand);
        var models = Values.ListOf(() => EntityValues.RandomModel(brand));
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
        var brand = EntityValues.RandomBrand();
        sut.SetupBrand(brand);
        var models = Values.ListOf(() => EntityValues.RandomModel(brand));
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
        var brand = EntityValues.RandomBrand();
        sut.SetupBrand(brand);
        var models = Values.ListOf(() => EntityValues.RandomModel(brand));
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
}