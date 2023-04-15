using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Infrastructure;

public class VehicleRepository : IVehicleRepository
{
    private readonly ApplicationDbContext _db;

    public VehicleRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        await _db.Vehicles.AddAsync(vehicle);
        await _db.SaveChangesAsync();
    }

    public async Task<Vehicle?> FindByVinAsync(string vin)
    {
        return await _db.Vehicles.FirstOrDefaultAsync(vehicle => vehicle.VIN == vin);
    }
}