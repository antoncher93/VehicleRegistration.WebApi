using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Infrastructure;

public class ModelRepository : IModelRepository
{
    private readonly ApplicationDbContext _db;

    public ModelRepository(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task AddModelAsync(
        Types.Model model)
    {
        await _db.Models.AddAsync(model);
        await _db.SaveChangesAsync();
    }

    public Task<List<Model>> GetModelsOfBrandAsync(
        int brandId)
    {
        return _db.Models
            .Where(model => model.BrandId == brandId)
            .ToListAsync();
    }

    public Task<Model?> GetByIdAsync(int id)
    {
        return _db.Models.FirstOrDefaultAsync(m => m.Id == id);
    }
}