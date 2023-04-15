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

    public static Engine RandomEngine(
        List<Model>? models = default)
    {
        var randomEngineType = Enum
            .GetValues<EngineType>()
            .MinBy(_ => Values.RandomInt());

        return new Engine()
        {
            Type = randomEngineType,
            Models = models,
            HorsePower = Values.RandomDouble(0.5, 9999.9),
            Volume = randomEngineType == EngineType.Electrical ? null : Values.RandomDouble(0.1, 99.9)
        };
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
}