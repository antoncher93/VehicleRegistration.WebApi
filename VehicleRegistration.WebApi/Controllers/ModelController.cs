using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

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

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] AddModelRequest request)
    {
        var models = await _modelRepository.GetModelsOfBrandAsync(request.BrandId);

        if (models.Any(m => m.ModelName == request.Name))
        {
            return this.BadRequest("Такая модель уже есть в базе");
        }
        
        var model = new Model()
        {
            ModelName = request.Name,
            BrandId = request.BrandId,
        };

        await _modelRepository.AddModelAsync(model);

        return this.Ok(model);
    }
}