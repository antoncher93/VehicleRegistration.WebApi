using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IEngineRepository
{
    Task<Engine?> FindByIdAsync(int id);
    
    Task<List<Engine>> GetEnginesAsync(IEnumerable<int> ids);

    Task AddEngineAsync(Engine engine);
}