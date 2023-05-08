﻿using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.DB;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Infrastructure;

public class EngineRepository : IEngineRepository
{
    private readonly ApplicationDbContext _db;

    public EngineRepository(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public Task<Engine?> FindByIdAsync(
        int id)
    {
        return _db.Engines.FirstOrDefaultAsync(engine => engine.Id == id);
    }

    public async Task<List<Engine>> GetEnginesAsync(
        IEnumerable<int> ids)
    {
        return await _db.Engines
            .Where(engine => ids.Any(id => id == engine.Id))
            .ToListAsync();
    }

    public async Task AddEngineAsync(
        Engine engine)
    {
        await _db.Engines.AddAsync(engine);
        await _db.SaveChangesAsync();
    }
}