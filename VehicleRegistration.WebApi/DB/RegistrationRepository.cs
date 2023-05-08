using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.DB;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Infrastructure;

public class RegistrationRepository : IRegistrationRepository
{
    private readonly ApplicationDbContext _db;

    public RegistrationRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Registration registration)
    {
        await _db.Registrations.AddAsync(registration);
        await _db.SaveChangesAsync();
    }

    public async Task<List<Registration>> FindRegistrationsByRegNumberAsync(
        string regNumber)
    {
        return await _db.Registrations.Where(
                registration => string.Equals(registration.RegNumber, regNumber.ToUpper()))
            .Include(r => r.Vehicle)
            .ThenInclude(v => v.Model)
            .ThenInclude(m => m.Brand)
            .Include(r => r.Vehicle)
            .ThenInclude(r => r.Engine)
            .ThenInclude(e => e.Type)
            .Include(r => r.Vehicle)
            .ThenInclude(v => v.Body)
            .Include(r => r.Owner)
            .ToListAsync();
    }

    public async Task<List<Registration>> FindRegistrationByOwnerId(int ownerId)
    {
        return await _db.Registrations.Where(
                registration => registration.OwnerId == ownerId)
            .Include(r => r.Vehicle)
            .ThenInclude(v => v.Model)
            .ThenInclude(m => m.Brand)
            .Include(r => r.Vehicle)
            .ThenInclude(r => r.Engine)
            .ThenInclude(e => e.Type)
            .Include(r => r.Vehicle)
            .ThenInclude(v => v.Body)
            .Include(r => r.Owner)
            .ToListAsync();
    }

    public async Task<List<string>> GetBusyRegNumbersAsync()
    {
        return await _db.Registrations
            .Where(r => r.IsActive)
            .Select(r => r.RegNumber)
            .ToListAsync();
    }

    public async Task<Registration?> FindByIdAsync(int registrationId)
    {
        return await _db.Registrations.Where(
                r => r.Id == registrationId)
            .Include(r => r.Vehicle)
            .ThenInclude(v => v.Model)
            .ThenInclude(m => m.Brand)
            .Include(r => r.Vehicle)
            .ThenInclude(r => r.Engine)
            .ThenInclude(e => e.Type)
            .Include(r => r.Vehicle)
            .ThenInclude(v => v.Body)
            .Include(r => r.Owner)
            .FirstOrDefaultAsync();
    }

    public async Task<List<Registration>> FindRegistrationByVehicleId(int vehicleId)
    {
        return await _db.Registrations
            .Where(r => r.VehicleId == vehicleId)
            .Include(r => r.Vehicle)
            .ThenInclude(v => v.Model)
            .ThenInclude(m => m.Brand)
            .Include(r => r.Vehicle)
            .ThenInclude(r => r.Engine)
            .ThenInclude(e => e.Type)
            .Include(r => r.Vehicle)
            .ThenInclude(v => v.Body)
            .Include(r => r.Owner)
            .ToListAsync();
    }

    public async Task DeactivateAsync(Registration registration)
    {
        registration.IsActive = false;
        await _db.SaveChangesAsync();
    }
}