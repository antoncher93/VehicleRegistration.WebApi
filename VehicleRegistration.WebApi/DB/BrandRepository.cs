using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.DB;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Infrastructure;

public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _db;

    public BrandRepository(
        ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Brand>> GetBrandsAsync()
    {
        var resultList = new List<Brand>();
        var brandsNames = await _db.Brands?
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

    public async Task<Brand?> FindByIdAsync(int id)
    {
        return await _db.Brands.FirstOrDefaultAsync(brand => brand.Id == id);
    }

    public async Task AddAsync(Brand brand)
    {
        await _db.Brands.AddAsync(brand);
        await _db.SaveChangesAsync();
    }

    public async Task AddModelAsync(Model model)
    {
        await _db.Models!.AddAsync(model);
        await _db.SaveChangesAsync();
    }
}