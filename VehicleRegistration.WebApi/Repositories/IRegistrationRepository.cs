using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IRegistrationRepository
{
    public Task AddAsync(
        Registration registration);

    Task<Registration?> FindRegistrationByRegNumber(
        string regNumber);

    Task<List<string>> GetAllRegNumbersAsync();
    
    Task<Registration?> FindByIdAsync(
        int registrationId);

    Task UpdateAsync(
        Registration registration);
}