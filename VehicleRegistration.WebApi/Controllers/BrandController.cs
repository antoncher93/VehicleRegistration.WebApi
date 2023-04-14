using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Repositories;
using VehicleRegistration.WebApi.RequestModels;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Controllers;

[ApiController]
[Route("api/brand")]
public class BrandController : ControllerBase
{
    private readonly IBrandRepository _brands;
    private readonly IModelRepository _models;
    private readonly IEngineRepository _engines;
    private readonly IBodyRepository _bodies;

    public BrandController(
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

    [HttpGet]
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

    [HttpPost("{brandName}")]
    public async Task<IActionResult> PostModelsAsync(
        string brandName,
        [FromBody] AddModelRequest request)
    {
        var brand = await _brands.GetBrandAsync(brandName);

        if (brand is null)
        {
            return this.NotFound();
        }

        var model = new Types.Model()
        {
            ModelName = request.Name,
            Brand = brand,
        };

        if (request.EngineIds != null)
        {
            model.Engines = await _engines.GetEnginesAsync(request.EngineIds);
        }
        
        if (request.BodyIds != null)
        {
            var bodies = await _bodies.GetBodiesAsync(request.BodyIds);
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