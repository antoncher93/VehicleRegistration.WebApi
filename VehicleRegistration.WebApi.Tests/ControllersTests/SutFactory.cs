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
        var vehicles = new VehicleRepository(db);
        var owners = new OwnerRepository(db);
        var registrations = new RegistrationRepository(db);

        var brandController = new BrandController(
            brands: brands);

        var modelController = new ModelController(
            modelRepository: models,
            brandRepository: brands,
            bodyRepository: bodies,
            engineRepository: engines);

        var engineController = new EngineController(
            engines: engines);

        var bodyController = new BodyController(bodies);

        var vehicleController = new VehicleController(
            vehicleRepository: vehicles,
            modelRepository: models,
            engineRepository: engines,
            bodyRepository: bodies);

        var ownerController = new OwnerController(owners);

        var registrationController = new RegistrationController(
            registrationRepository: registrations,
            vehicleRepository: vehicles,
            ownerRepository: owners);
        
        return new Sut(
            db: db,
            brandController: brandController,
            modelController: modelController,
            engineController: engineController,
            bodyController: bodyController,
            vehicleController: vehicleController,
            ownerController: ownerController,
            registrationController: registrationController);
    }
}