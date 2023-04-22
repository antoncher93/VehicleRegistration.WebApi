using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Infrastructure;

public class EngineTypeRepository : IEngineTypeRepository
{
    private readonly ApplicationDbContext _db;

    public EngineTypeRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<EngineType>> GetAllAsync()
    {
        return await _db.EngineTypes.ToListAsync();
    }

    public async Task<EngineType?> GetByIdAsync(int id)
    {
        return await _db.EngineTypes.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AddAsync(EngineType engineType)
    {
        await _db.EngineTypes.AddAsync(engineType);
        await _db.SaveChangesAsync();
    }
}