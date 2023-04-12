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

    public async Task<Model?> GetByIdAsync(int modelId)
    {
        return await _db.Models
            .Include(model => model.Bodies)
            .Include(model => model.Engines)
            .FirstOrDefaultAsync(model => model.Id == modelId);
    }

    public async Task<Types.Model?> GetByNameAsync(
        string brandName,
        string modelName)
    {
        return await _db.Models
            .Include(model => model.Bodies)
            .Include(model => model.Engines)
            .FirstOrDefaultAsync(model 
                => model.Brand.Name == brandName && model.ModelName == modelName);
    }

    public async Task AddEngineForModelAsync(
        int id,
        Engine engine)
    {
        var model = await _db.Models.FirstOrDefaultAsync(model => model.Id == id);
        if (model != null)
        {
            model.Engines.Add(engine);
            await _db.SaveChangesAsync();
        }
    }

    public Task AddBodyForModel(
        int id,
        Body body)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Types.Model model)
    {
        _db.Models.Update(model);
        await _db.SaveChangesAsync();
    }
}