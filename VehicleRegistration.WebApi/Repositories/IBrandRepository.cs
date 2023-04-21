using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IBrandRepository
{
    Task<List<string>> GetBrandsNamesAsync();

    Task<Brand?> GetBrandAsync(
        string brandName);

    Task<Brand?> FindByIdAsync(int id);

    Task AddAsync(
        Brand brand);
}