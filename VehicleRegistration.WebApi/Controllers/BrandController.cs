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

    public BrandController(
        IBrandRepository brands)
    {
        _brands = brands;
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
}