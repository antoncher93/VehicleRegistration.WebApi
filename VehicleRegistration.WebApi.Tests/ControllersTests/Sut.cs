using VehicleRegistration.WebApi.Controllers;
using VehicleRegistration.WebApi.Infrastructure;
using VehicleRegistration.WebApi.Tests.Extensions;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public class Sut : IDisposable
{
    private readonly ApplicationDbContext _db;
    public Sut(
        ApplicationDbContext db,
        BrandController brandController,
        ModelController modelController,
        EngineController engineController,
        BodyController bodyController,
        VehicleController vehicleController,
        OwnerController ownerController,
        RegistrationController registrationController)
    {
        BrandController = brandController;
        ModelController = modelController;
        EngineController = engineController;
        BodyController = bodyController;
        VehicleController = vehicleController;
        OwnerController = ownerController;
        RegistrationController = registrationController;
        _db = db;
    }

    public BrandController BrandController { get; }
    public ModelController ModelController { get; }
    public EngineController EngineController { get; }
    public BodyController BodyController { get; }
    public VehicleController VehicleController { get; }
    public OwnerController OwnerController { get; }
    public RegistrationController RegistrationController { get; }

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

    public void SetupBodies(List<Body> bodies)
    {
        _db.Bodies.Clear();
        _db.Bodies.AddRange(bodies);
        _db.SaveChanges();
    }

    public void SetupVehicle(Vehicle vehicle)
    {
        _db.Vehicles.Add(vehicle);
        _db.SaveChanges();
    }

    public void SetupOwner(Owner owner)
    {
        _db.Owners.Add(owner);
        _db.SaveChangesAsync();
    }

    public void Dispose()
    {
        _db.Dispose();
    }

    public void ClearRegistrations()
    {
        _db.Registrations.Clear();
        _db.SaveChanges();
    }
}