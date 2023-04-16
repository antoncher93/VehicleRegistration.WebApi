using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IEngineRepository
{
    Task AddEngineAsync(Engine engine);
}