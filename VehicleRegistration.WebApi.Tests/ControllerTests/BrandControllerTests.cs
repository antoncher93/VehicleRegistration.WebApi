using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Controllers;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Tests.Fakes;

namespace VehicleRegistration.WebApi.Tests.ControllerTests;

public class BrandControllerTests
{
    private readonly FakeBrandRepository _brandRepository;
    private readonly BrandController _brandController;

    public BrandControllerTests()
    {
        _brandRepository = new FakeBrandRepository();
        _brandController = new BrandController(_brandRepository);
    }

    // Тест проверяет, что GET метод возвращает
    // все бренды, которые есть в базе данных 
    [Fact]
    public async Task GetMethodReturnsAllBrands()
    {
        var brands = new[]
        {
            new Brand() { Name = "Brand1" },
            new Brand() { Name = "Brand2" },
            new Brand() { Name = "Brand3" },
        };
        
        _brandRepository.SetupManyBrands(brands);
        
        var result = await _brandController.GetBrandsAsync();

        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(brands);
    }

    // тест проверяет, что Post метод возвращает BadRequest
    // в том случае, если попытаться добавить бренд, который
    // уже существует в базе данных
    [Fact]
    public async Task PostMethodReturnsBadRequest()
    {
        var brand = new Brand()
        {
            Name = "TestBrand",
        };

        await _brandRepository.AddAsync(brand);

        var result = await _brandController.PostAsync(brand);

        result
            .As<BadRequestObjectResult>()
            .Value
            .Should()
            .Be("Такой бренд уже существует в базе");
    }

    // тест проверяет то, что Post метод возвращает OK
    // если данные переданы корректно 
    [Fact]
    public async Task PostMethodReturnsOk()
    {
        var brand = new Brand()
        {
            Name = "TestBrand2",
        };
        
        var result = await _brandController.PostAsync(brand);

        result.Should().BeOfType<OkObjectResult>();
    }
}