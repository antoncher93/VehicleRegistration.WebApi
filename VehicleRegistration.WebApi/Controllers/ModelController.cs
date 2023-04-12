using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/model")]
public class ModelController : ControllerBase
{
    private readonly IModelRepository _modelRepository;
    private readonly IBrandRepository _brandRepository;

    public ModelController(
        IModelRepository modelRepository,
        IBrandRepository brandRepository)
    {
        _modelRepository = modelRepository;
        _brandRepository = brandRepository;
    }

    [HttpGet("{brand}")]
    public async Task<IActionResult> GetModelsAsync(
        string brand)
    {
        var existingBrand = await _brandRepository.GetBrandAsync(brand);

        if (existingBrand is null)
        {
            return this.NotFound();
        }
        
        var models = await _modelRepository
            .GetModelsOfBrandAsync(existingBrand.Id);

        return this.Ok(models);
    }
    
    [HttpGet("{brandName}/{modelName}")]
    public async Task<IActionResult> GetModelDetailsAsync(
        string brandName,
        string modelName)
    {
        var model = await _modelRepository.GetByNameAsync(
            brandName: brandName,
            modelName: modelName);

        if (model is null)
        {
            return this.NotFound();
        }

        return this.Ok(model);
    }
}