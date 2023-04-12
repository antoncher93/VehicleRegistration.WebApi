using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/vehicle")]
public class VehicleController : ControllerBase
{
    private readonly IBrandRepository _brands;
    private readonly IModelRepository _models;
    private readonly IEngineRepository _engines;
    private readonly IBodyRepository _bodies;

    public VehicleController(
        IBrandRepository brands,
        IModelRepository models,
        IEngineRepository engines,
        IBodyRepository bodies)
    {
        _brands = brands;
        _models = models;
        _engines = engines;
        _bodies = bodies;
    }

    [HttpGet("brands")]
    public async Task<IActionResult> GetBrandsAsync()
    {
        var resultList = await _brands.GetBrandsNamesAsync();

        return this.Ok(resultList);
    }
    
    [HttpPost("brands")]
    public async Task<IActionResult> PostAsync(
        [FromBody] AddBrandRequestBody requestBody)
    {
        var brand = await _brands.GetBrandAsync(
            brandName: requestBody.Name);

        if (brand != null)
        {
            return this.BadRequest("Такой бренд уже существует в базе");
        }

        brand = new Brand()
        {
            Name = requestBody.Name,
            Models = new List<Types.Model>(),
        };
        
        await _brands.AddAsync(brand);

        return this.Ok(brand);
    }
    
    [HttpGet("{brandName}")]
    public async Task<IActionResult> GetModelsAsync(
        string brandName)
    {
        var brand = await _brands.GetBrandAsync(brandName);

        if (brand is null)
        {
            return this.NotFound();
        }

        return this.Ok(brand);
    }
    
    [HttpGet("{brandName}/{modelName}")]
    public async Task<IActionResult> GetModelDetailsAsync(
        string brandName,
        string modelName)
    {
        var model = await _models.GetByNameAsync(
            brandName: brandName,
            modelName: modelName);

        if (model is null)
        {
            return this.NotFound();
        }

        return this.Ok(model);
    }

    [HttpPost("{brandName}")]
    public async Task<IActionResult> PostModelsAsync(
        string brandName,
        [FromBody] AddModelRequestBody requestBody)
    {
        var brand = await _brands.GetBrandAsync(brandName);

        if (brand is null)
        {
            return this.NotFound();
        }

        var model = new Types.Model()
        {
            ModelName = requestBody.Name,
            Brand = brand,
        };

        if (requestBody.EngineIds != null)
        {
            model.Engines = await _engines.GetEnginesAsync(requestBody.EngineIds);
        }
        
        if (requestBody.BodyIds != null)
        {
            var bodies = await _bodies.GetBodiesAsync(requestBody.BodyIds);
            model.Bodies =  bodies;
        }

        await _brands.AddModelAsync(model);

        return this.Ok(model);
    }

    [HttpPost("model/{modelId}")]
    public async Task<IActionResult> UpdateModelAsync(
        int modelId,
        [FromBody] UpdateModelRequestBody requestBody)
    {
        var model = await _models.GetByIdAsync(modelId);

        if (model is null)
        {
            return this.NotFound();
        }

        if (requestBody.EngineIds != null)
        {
            model.Engines = await _engines.GetEnginesAsync(requestBody.EngineIds);
        }

        if (requestBody.BodyIds != null)
        {
            model.Bodies = await _bodies.GetBodiesAsync(requestBody.BodyIds);
        }

        await _models.UpdateAsync(model);

        return this.Ok(model);
    }
}