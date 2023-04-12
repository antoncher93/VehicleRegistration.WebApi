using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using VehicleRegistration.WebApi.Controllers;
using VehicleRegistration.WebApi.Infrastructure;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class Sut : IDisposable
{
    private readonly ApplicationDbContext _db;
    public Sut(ApplicationDbContext db, BrandController brandController, ModelController modelController)
    {
        BrandController = brandController;
        ModelController = modelController;
        _db = db;
    }

    public BrandController BrandController { get; }
    public ModelController ModelController { get; }

    public void SetupBrand(
        Brand brand)
    {
        _db.Brands.Add(brand);
        _db.SaveChanges();
    }

    public void SetupModels(IEnumerable<Model> models)
    {
        _db.Models.AddRange(models);
        _db.SaveChanges();
    }

    public Brand? FindBrandByName(string brandName)
    {
        return _db.Brands.FirstOrDefault(brand => brand.Name == brandName);
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}