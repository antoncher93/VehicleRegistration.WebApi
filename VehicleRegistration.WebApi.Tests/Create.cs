using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Tests;

public static class Create
{
    public static Brand RandomBrand()
    {
        return new Brand()
        {
            Name = Values.RandomString(),
        };
    }

    public static Model RandomModel(
        Brand? brand = default)
    {
        brand ??= RandomBrand();
        return new Model()
        {
            Brand = brand,
            BrandId = brand.Id,
            ModelName = Values.RandomString(),
        };
    }

    public static Engine RandomEngine()
    {
        var randomEngineType = new EngineType() { Name = Values.RandomString() };
        return new Engine(
            number: Values.RandomString(),
            type: randomEngineType,
            horsePower: Values.RandomDouble(0.5, 9999.9),
            volume: Values.RandomDouble(0.1, 9.9));
    }

    public static Body RandomBody()
    {
        return new Body()
        {
            Name = Values.RandomString(),
        };
    }

    public static Vehicle RandomVehicle(
        string? vin = default)
    {
        var model = Create.RandomModel();
        var body = Create.RandomBody();
        var engine = Create.RandomEngine();
        return new Vehicle(
            vin: vin ?? Values.RandomString(),
            model: model,
            body: body,
            engine: engine,
            transmission: Values.RandomEnum<Transmission>(),
            color: Values.RandomString());
    }

    public static Owner RandomOwner()
    {
        return new Owner()
        {
            FirstName = Values.RandomString(),
            LastName = Values.RandomString(),
            MiddleName = Values.RandomString(),
        };
    }

    public static Registration RandomRegistration(
        bool? isActive = default)
    {
        return new Registration(
            vehicle: Create.RandomVehicle(),
            owner: Create.RandomOwner(),
            regNumber: Values.RandomString(),
            isActive: isActive ?? true);
    }
}