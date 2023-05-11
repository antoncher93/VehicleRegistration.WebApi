using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.DB;

public class EngineRepository : IEngineRepository
{
    private readonly ApplicationDbContext _db;

    public EngineRepository(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddEngineAsync(
        Engine engine)
    {
        await _db.Engines.AddAsync(engine);
        await _db.SaveChangesAsync();
    }

    public async Task<Engine?> FindByNumberAsync(string engineNumber)
    {
        return await _db.Engines.FirstOrDefaultAsync(
            e => e.Number == engineNumber);
    }
}