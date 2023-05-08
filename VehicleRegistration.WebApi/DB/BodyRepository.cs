using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.DB;

public class BodyRepository : IBodyRepository
{
    private readonly ApplicationDbContext _db;

    public BodyRepository(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Body>> GetBodiesAsync(
        IEnumerable<int>? bodyIds = default)
    {
        if (bodyIds != null)
        {
            return await _db.Bodies
                .Where(body => bodyIds.Any(id => id == body.Id))
                .ToListAsync();
        }
            
        return await _db.Bodies.ToListAsync();
    }

    public async Task AddBodyAsync(
        Body body)
    {
        await _db.Bodies.AddAsync(body);
        await _db.SaveChangesAsync();
    }

    public async Task<Body?> FindByIdAsync(int id)
    {
        return await _db.Bodies
            .FirstOrDefaultAsync(body => body.Id == id);
    }
}