using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Models;
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

    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] Model model)
    {
        var models = await _modelRepository.GetModelsOfBrandAsync(model.BrandId);

        var isSameModelExists = models.Any(m => m.ModelName == model.ModelName);
        
        if (isSameModelExists)
        {
            return this.BadRequest("Такая модель уже есть в базе");
        }

        await _modelRepository.AddModelAsync(model);

        return this.Ok(model);
    }
}