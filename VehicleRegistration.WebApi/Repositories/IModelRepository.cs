using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IModelRepository
{
    Task AddModelAsync(Model model);

    Task<List<Model>> GetModelsOfBrandAsync(int brandId);
    Task<Model?> GetByIdAsync(int id);
}