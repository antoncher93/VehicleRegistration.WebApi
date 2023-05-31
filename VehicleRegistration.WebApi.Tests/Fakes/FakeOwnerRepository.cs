using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Tests.Fakes;

public class FakeOwnerRepository : IOwnerRepository
{
    private readonly List<Owner> _owners = new();
    public Task AddAsync(Owner owner)
    {
        _owners.Add(owner);

        return Task.CompletedTask;
    }

    public Task<Owner?> FindByPassportDataAsync(string series, string number)
    {
        var result = _owners.FirstOrDefault(o => o.PassportSeries == series
                                                 && o.PassportNumber == number);
        return Task.FromResult(result);
    }
}