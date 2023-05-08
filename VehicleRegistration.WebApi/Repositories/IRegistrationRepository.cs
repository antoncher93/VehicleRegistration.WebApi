using VehicleRegistration.WebApi.Models;

namespace VehicleRegistration.WebApi.Repositories;

public interface IRegistrationRepository
{
    public Task AddAsync(
        Registration registration);

    Task<List<Registration>> FindRegistrationsByRegNumberAsync(
        string regNumber);

    Task<List<string>> GetBusyRegNumbersAsync();
    
    Task<Registration?> FindByIdAsync(
        int registrationId);

    Task<List<Registration>> FindRegistrationByVehicleId(
        int vehicleId);
    
    Task<List<Registration>> FindRegistrationByOwnerId(
        int ownerId);

    Task DeactivateAsync(Registration registration);
}