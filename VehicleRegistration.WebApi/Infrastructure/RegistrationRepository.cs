using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.Types;

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
    }

    public async Task<Registration?> FindRegistrationByRegNumber(string regNumber)
    {
        return await _db.Registrations.FirstOrDefaultAsync(
            registration => string.Equals(registration.RegNumber, regNumber));
    }

    public async Task<List<string>> GetAllRegNumbersAsync()
    {
        return await _db.Registrations
            .Select(r => r.RegNumber)
            .ToListAsync();
    }

    public async Task<Registration?> FindByIdAsync(int registrationId)
    {
        return await _db.Registrations
            .FirstOrDefaultAsync(r => r.Id == registrationId);
    }

    public async Task UpdateAsync(Registration registration)
    {
        _db.Registrations.Update(registration);
        await _db.SaveChangesAsync();
    }
}