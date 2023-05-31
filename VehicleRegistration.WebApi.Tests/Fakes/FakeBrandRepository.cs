using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Tests.Fakes;

public class FakeBrandRepository : IBrandRepository
{
    private readonly List<Brand> _brands = new();
    
    public Task<List<Brand>> GetBrandsAsync()
    {
        return Task.FromResult(_brands.ToList());
    }

    public Task<Brand?> GetBrandAsync(string brandName)
    {
        var result = _brands.FirstOrDefault(b => b.Name == brandName);
        return Task.FromResult(result);
    }

    public Task AddAsync(Brand brand)
    {
        _brands.Add(brand);
        return Task.CompletedTask;
    }

    public void SetupManyBrands(IEnumerable<Brand> brands)
    {
        _brands.AddRange(brands);
    }
}