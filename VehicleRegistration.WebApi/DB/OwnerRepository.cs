using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.DB;

public class OwnerRepository : IOwnerRepository
{
    private readonly ApplicationDbContext _db;

    public OwnerRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Owner owner)
    {
        await _db.Owners.AddAsync(owner);
        await _db.SaveChangesAsync();
    }

    public async Task<Owner?> FindByIdAsync(int id)
    {
        return await _db.Owners
            .FirstOrDefaultAsync(
                owner => owner.Id == id);
    }

    public async Task<Owner?> FindByPassportDataAsync(int series, int number)
    {
        return await _db.Owners
            .FirstOrDefaultAsync(
                owner => owner.PassportSeries == series
                && owner.PassportNumber == number);
    }
}