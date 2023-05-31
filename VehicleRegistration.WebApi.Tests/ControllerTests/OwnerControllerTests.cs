using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Controllers;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Tests.Fakes;

namespace VehicleRegistration.WebApi.Tests.ControllerTests;

public class OwnerControllerTests
{
    private readonly FakeOwnerRepository _fakeOwnerRepository;
    private readonly OwnerController _ownerController;

    public OwnerControllerTests()
    {
        _fakeOwnerRepository = new FakeOwnerRepository();
        _ownerController = new OwnerController(_fakeOwnerRepository);
    }

    // проверяет, что контроллер возвращает существующего в хранилище пользователя
    [Fact]
    public async Task GetMethodReturnsExistingOwner()
    {
        // AAA
        
        // arrange
        var owner = new Owner()
        {
            FirstName = "TestFirstName",
            LastName = "TestLatsName",
            MiddleName = "TestMiddleName",
            PassportSeries = "TestPasportSeries",
            PassportNumber = "TestPAssportNumber",
        };

        await _fakeOwnerRepository.AddAsync(owner);

        // act
        var result = await _ownerController.GetAsync(
            passportSeries: "TestPasportSeries",
            passportNumber: "TestPAssportNumber");

        // assert
        result                      // результат
            .As<OkObjectResult>()   // должен быть типа OkObjectResult
            .Value                  // а его значение
            .Should()               // должно быть
            .BeEquivalentTo(owner); // эквиволетно тому, которое мы добавили в хранилище
    }

    
    // проверяет, что контроллер возвращает ошибку NotFound,
    // если запрошен владелец, которого нет в хранилище 
    [Fact]
    public async Task GetMethodReturnsNotFound()
    {
        // arrange & act
        var result = await _ownerController.GetAsync(
            passportSeries: "otherPasportSeries",
            passportNumber: "otherPAssportNumber");

        // assert
        result
            .Should()
            .BeOfType<NotFoundResult>();
    }

    // проверяет, что PostMethod возвращает OK
    // при добавлении нового собственника
    [Fact]
    public async Task PostMethodReturnsOk()
    {
        // arrange & act
        var result = await _ownerController.PostAsync(
            owner: new Owner()
            {
                FirstName = "TestFirstName",
                LastName = "TestLatsName",
                MiddleName = "TestMiddleName",
                PassportSeries = "TestPasportSeries",
                PassportNumber = "TestPAssportNumber",
            });

        // assert
        result
            .Should()
            .BeOfType<OkObjectResult>();
    }
    
    // проверяет, что PostMethod возвращает BadRequest
    // при добавлении собственника, который уже есть в базе
    [Fact]
    public async Task PostMethodReturnsBadRequest()
    {
        // arrange
        var owner = new Owner()
        {
            FirstName = "TestFirstName",
            LastName = "TestLatsName",
            MiddleName = "TestMiddleName",
            PassportSeries = "TestPasportSeries",
            PassportNumber = "TestPAssportNumber",
        };

        await _fakeOwnerRepository.AddAsync(owner);
        
        // act
        var result = await _ownerController.PostAsync(owner);

        // assert
        result
            .Should()
            .BeOfType<BadRequestObjectResult>();
    }
}