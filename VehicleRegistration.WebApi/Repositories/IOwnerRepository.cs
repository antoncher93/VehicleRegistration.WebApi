using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IOwnerRepository
{
    Task AddAsync(Owner owner);

    Task<Owner?> FindByIdAsync(int id);
}