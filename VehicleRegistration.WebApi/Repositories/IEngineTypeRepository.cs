using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IEngineTypeRepository
{
    Task<List<EngineType>> GetAllAsync();
    Task<EngineType?> GetByIdAsync(int id);
    Task AddAsync(EngineType engineType);
}