using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IBrandRepository
{
    Task<List<Brand>> GetBrandsAsync();

    Task<Brand?> GetBrandAsync(
        string brandName);

    Task AddAsync(
        Brand brand);
}