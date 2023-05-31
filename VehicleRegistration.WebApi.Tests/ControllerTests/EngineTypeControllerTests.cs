using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Controllers;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Tests.Fakes;

namespace VehicleRegistration.WebApi.Tests.ControllerTests;

public class EngineTypeControllerTests
{
    private readonly EngineTypeController _engineTypeController;
    private readonly FakeEngineTypeRepository _engineTypeRepository;

    public EngineTypeControllerTests()
    {
        _engineTypeRepository = new FakeEngineTypeRepository();
        _engineTypeController = new EngineTypeController(_engineTypeRepository);
    }

    // тест проверяет, что GET метод возвращает все 
    // типы двигателей, которые есть в базе данных
    [Fact]
    public async Task GetMethodReturnsAllEngineTypes()
    {
        var engineTypes = new[]
        {
            new EngineType() { Name = "EngineType1" },
            new EngineType() { Name = "EngineType2" },
            new EngineType() { Name = "EngineType3" },
        };

        _engineTypeRepository.SetupManyEngineTypes(engineTypes);

        var result = await _engineTypeController.GetAsync();

        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(engineTypes);
    }

    // тест проверяет, что POST метод возвращает OK
    // если данные были переданы корректно
    [Fact]
    public async Task PostMethodReturnsOk()
    {
        var engineType = new EngineType() { Name = "TestEngineType" };

        var result = await _engineTypeController.PostAsync(engineType);

        result
            .Should()
            .BeOfType<OkObjectResult>();
    }
    
    // тест проверяет, что POST метод возвращает BadRequest
    // если попытаться добавить тип двигателя, который
    // уже есть в базе данных
    [Fact]
    public async Task PostMethodReturnsBadRequest()
    {
        var engineType = new EngineType() { Name = "TestEngineType" };

        await _engineTypeRepository.AddAsync(engineType);

        var result = await _engineTypeController.PostAsync(engineType);

        result
            .As<BadRequestObjectResult>()
            .Value
            .Should()
            .Be("Такой тип двигателя уже существует");
    }
}