using VehicleRegistration.WebApi.Models;

namespace VehicleRegistration.WebApi.Repositories;

public interface IOwnerRepository
{
    Task AddAsync(Owner owner);

    Task<Owner?> FindByIdAsync(int id);
    
    Task<Owner?> FindByFullNameAsync(string firstName, string lastName, string middleName);
}