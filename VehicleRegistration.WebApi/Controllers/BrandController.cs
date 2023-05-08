using Microsoft.AspNetCore.Mvc;
using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

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

    [HttpGet("list")]
    public async Task<IActionResult> GetBrandsAsync()
    {
        var resultList = await _brands.GetBrandsAsync();

        return this.Ok(resultList);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync(
        [FromBody] Brand brand)
    {
        var brands = await _brands.GetBrandsAsync();

        // проверяем, есть ли такая марка в базе
        var isSameBrandExists = brands.Any(b => b.Name == brand.Name);

        if (isSameBrandExists)
        {
            // возвращаем ошибку запроса, и говорим, что нельзя добавлять в базу 2 одинаковые марки
            return this.BadRequest("Такой бренд уже существует в базе");
        }
        
        await _brands.AddAsync(brand);

        return this.Ok(brand);
    }
}