using Microsoft.EntityFrameworkCore;
using VehicleRegistration.WebApi.Controllers;
using VehicleRegistration.WebApi.Infrastructure;

namespace VehicleRegistration.WebApi.Tests.ControllersTests;

public static class SutFactory
{
    public static Sut Create()
    {
        var dbBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        var options = dbBuilder.UseSqlServer("Server=localhost,1433;Database=master;User Id=sa;Password=Pa23@!Ze7&;TrustServerCertificate=True;").Options;
        var db = new ApplicationDbContext(options);

        var brands = new BrandRepository(db);
        var engines = new EngineRepository(db);
        var models = new ModelRepository(db);
        var bodies = new BodyRepository(db);

        var brandController = new BrandController(
            brands: brands,
            models: null,
            engines: null,
            bodies: null);

        var modelController = new ModelController(
            modelRepository: models,
            brandRepository: brands);

        var engineController = new EngineController(
            engines: engines);
        
        return new Sut(
            db: db,
            brandController: brandController,
            modelController: modelController,
            engineController: engineController);
    }
}