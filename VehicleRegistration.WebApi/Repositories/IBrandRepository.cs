using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IBrandRepository
{
    Task<List<string>> GetBrandsNamesAsync();

    Task<Brand?> GetBrandAsync(
        string brandName);

    Task<Types.Model?> GetModelAsync(
        string brandName,
        string modelName);

    Task AddAsync(
        Brand brand);

    Task AddModelAsync(
        Types.Model model);
}