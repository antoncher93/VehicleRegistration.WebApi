using VehicleRegistration.WebApi.Controllers;
using VehicleRegistration.WebApi.Infrastructure;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class Sut : IDisposable
{
    private readonly ApplicationDbContext _db;
    public Sut(ApplicationDbContext db, BrandController brandController, ModelController modelController, EngineController engineController)
    {
        BrandController = brandController;
        ModelController = modelController;
        EngineController = engineController;
        _db = db;
    }

    public BrandController BrandController { get; }
    public ModelController ModelController { get; }
    public EngineController EngineController { get; }

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

    public void SetupEngines(List<Engine> engines)
    {
        _db.Engines.AddRange(engines);
        _db.SaveChanges();
    }

    public void Dispose()
    {
        _db.Dispose();
    }
}