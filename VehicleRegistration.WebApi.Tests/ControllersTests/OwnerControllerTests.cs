using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.RequestModels;
using Xunit;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class OwnerControllerTests
{
    [Fact]
    public async Task FindsOwnerByName()
    {
        using var sut = SutFactory.Create();
        var owner = Create.RandomOwner();
        sut.SetupOwner(owner);
        
        var result = await sut.OwnerController.GetByFullName(
            request: new GetOwnerByFullNameRequest()
            {
                FirstName = owner.FirstName,
                LastName = owner.LastName,
                MiddleName = owner.MiddleName,
            });

        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(owner);
    }

    [Fact]
    public async Task PostMethodReturnsOwner()
    {
        using var sut = SutFactory.Create();
        var expectedOwner = Create.RandomOwner();
        var result = await sut.OwnerController.PostAsync(
            request: new AddOwnerRequest()
            {
                FirstName = expectedOwner.FirstName,
                LastName = expectedOwner.LastName,
                MiddleName = expectedOwner.MiddleName,
            });

        result
            .As<OkObjectResult>()
            .Value
            .Should()
            .BeEquivalentTo(expectedOwner,
                config: options => options
                    .Excluding(owner => owner.Id));
    }
}