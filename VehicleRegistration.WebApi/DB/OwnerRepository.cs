using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.DB;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Infrastructure;

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

    public async Task<Owner?> FindByFullNameAsync(string firstName, string lastName, string middleName)
    {
        return await _db.Owners
            .FirstOrDefaultAsync(
                owner => owner.FirstName == firstName
                && owner.LastName == lastName
                && owner.MiddleName == middleName);
    }
}