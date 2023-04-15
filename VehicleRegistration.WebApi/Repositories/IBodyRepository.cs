using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IBodyRepository
{
    Task<List<Body>> GetBodiesAsync(
        IEnumerable<int>? bodyIds = default);

    Task AddBodyAsync(
        Body body);

    Task<Body?> FindByIdAsync(int id);
}