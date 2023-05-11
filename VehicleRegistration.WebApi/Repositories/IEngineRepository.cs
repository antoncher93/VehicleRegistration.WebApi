using VehicleRegistration.WebApi.Models;

namespace VehicleRegistration.WebApi.Repositories;

public interface IEngineRepository
{
    Task AddEngineAsync(Engine engine);
    Task<Engine?> FindByNumberAsync(string engineNumber);
}