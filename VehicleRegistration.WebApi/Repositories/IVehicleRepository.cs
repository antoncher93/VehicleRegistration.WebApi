using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle);

    Task<Vehicle?> FindByVinAsync(string vin);
}