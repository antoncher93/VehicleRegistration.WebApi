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
    private readonly IEngineRepository _engineRepository;
    private readonly IBodyRepository _bodyRepository;

    public ModelController(
        IModelRepository modelRepository,
        IBrandRepository brandRepository,
        IEngineRepository engineRepository, IBodyRepository bodyRepository)
    {
        _modelRepository = modelRepository;
        _brandRepository = brandRepository;
        _engineRepository = engineRepository;
        _bodyRepository = bodyRepository;
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

    public async Task<IActionResult> PostAsync(
        [FromBody] AddModelRequest request)
    {
        var model = new Model()
        {
            ModelName = request.Name,
            BrandId = request.BrandId,
        };

        var bodies = await _bodyRepository.GetBodiesAsync(request.BodyIds);
        
        model.Bodies = bodies;

        if (request.EngineIds != null)
        {
            var engines = await _engineRepository.GetEnginesAsync(request.EngineIds);
            model.Engines = engines;
        }

        await _modelRepository.AddModelAsync(model);

        return this.Ok(model);
    }
}