using VehicleRegistration.WebApi.Models;

namespace VehicleRegistration.WebApi.Repositories;

public interface IOwnerRepository
{
    Task AddAsync(Owner owner);

    Task<Owner?> FindByPassportDataAsync(int series, int number);
}