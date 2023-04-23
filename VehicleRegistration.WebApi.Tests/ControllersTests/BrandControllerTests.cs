using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;
using Xunit;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class BrandControllerTests
{
    [Fact]
    public async Task ReturnsBrand()
    {
        // arrange
        using var sut = SutFactory.Create();

        var brand = Create.RandomBrand();
        
        sut.SetupBrand(brand);

        // action
        var actionResult = await sut.BrandController.GetBrandsAsync();

        // assert
        var brandResult = Assert.IsType<OkObjectResult>(actionResult);

        brandResult.Value
            .As<IEnumerable<Brand>>()
            .Should()
            .Contain(brand);
    }

    [Fact]
    public async Task PostBrandReturnsBrandEntity()
    {
        using var sut = SutFactory.Create();

        var brandName = Values.RandomString();

        var result = await sut.BrandController.PostAsync(
            requestBody: new AddBrandRequestBody() { Name = brandName });

        var brandResult = Assert.IsType<OkObjectResult>(result);

        var expectedBrand = new Brand()
        {
            Name = brandName,
        };

        var actualBrand = sut.FindBrandByName(brandName);

        brandResult.Value
            .Should()
            .BeEquivalentTo(expectedBrand,
                config: options => options.Excluding(brand => brand.Id));

        actualBrand
            .Should()
            .BeEquivalentTo(brandResult.Value);
    }
    
    [Fact]
    public async Task PostBrandReturnsBadRequestForBrandAlreadyExists()
    {
        using var sut = SutFactory.Create();
        var existingBrand = Create.RandomBrand();
        sut.SetupBrand(existingBrand);
        var result = await sut.BrandController.PostAsync(
            requestBody: new AddBrandRequestBody() { Name = existingBrand.Name });

        result.As<BadRequestObjectResult>()
            .Should()
            .NotBeNull();
    }
}