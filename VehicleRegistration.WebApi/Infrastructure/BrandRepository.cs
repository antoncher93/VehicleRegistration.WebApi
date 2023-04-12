using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Infrastructure;

internal class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _db;

    public BrandRepository(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<string>> GetBrandsNamesAsync()
    {
        var resultList = new List<string>();
        var brandsNames = await _db.Brands?
            .Select(brand => brand.Name)
            .ToListAsync()!;
        
        resultList.AddRange(brandsNames);

         return resultList;
    }

    public async Task<Brand?> GetBrandAsync(string brandName)
    {
        var brand = await _db.Brands
            .Where(brand => brand.Name == brandName)
            .Include(b => b.Models)
            .FirstOrDefaultAsync(brand => brand.Name == brandName);

        return brand;
    }

    public async Task<Types.Model?> GetModelAsync(
        string brandName,
        string modelName)
    {
        var brand = await _db.Brands.FirstOrDefaultAsync(b => b.Name == brandName);

        if (brand is null)
            return null;

        var model = await _db.Models
            .Include(m => m.Bodies)
            .Include(m => m.Engines)
            .FirstOrDefaultAsync(m => m.ModelName == modelName);

        return model;
    }

    public async Task AddAsync(Brand brand)
    {
        if (_db.Brands != null)
        {
            await _db.Brands.AddAsync(brand);
            await _db.SaveChangesAsync();
        }
    }

    public async Task AddModelAsync(Types.Model model)
    {
        await _db.Models!.AddAsync(model);
        await _db.SaveChangesAsync();
    }

    private void AddInitData()
    {
        var camry40 = new Types.Model() { ModelName = "Camry 40" };

        var toyota = new Brand()
        {
            Name = "Toyota",
            Models = new List<Types.Model>()
            {
                camry40,
            }
        };

        _db.Models.Add(camry40);
        _db.Brands.Add(toyota);
        _db.SaveChanges();
    }
}