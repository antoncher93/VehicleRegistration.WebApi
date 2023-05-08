using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.DB;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

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
        return await _db.Vehicles
            .Where(vehicle => vehicle.VIN == vin)
            .Include(vehicle => vehicle.Engine)
            .ThenInclude(engine => engine.Type)
            .Include(vehicle => vehicle.Model)
            .ThenInclude(model => model.Brand)
            .Include(vehicle => vehicle.Body)
            .FirstOrDefaultAsync();
    }
}