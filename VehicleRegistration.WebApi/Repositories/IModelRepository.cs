using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Repositories;

public interface IModelRepository
{
    Task AddModelAsync(
        Types.Model model);

    Task<Types.Model?> GetByIdAsync(
        int modelId);
    
    Task<Types.Model?> GetByNameAsync(
        string brandName,
        string modelName);

    Task AddEngineForModelAsync(
        int id,
        Engine engine);

    Task AddBodyForModel(
        int id,
        Body body);

    Task UpdateAsync(
        Types.Model model);

    Task<List<Model>> GetModelsOfBrandAsync(int brandId);
}