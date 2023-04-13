using VehicleRegistration.WebApi.Tests.Extensions;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.Tests;

public static class EntityValues
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
        return new Model()
        {
            Brand = brand ?? RandomBrand(),
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
            Models = models ?? EntityValues.RandomModel().AsList(),
            HorsePower = Values.RandomDouble(0.5, 9999.9),
            Volume = randomEngineType == EngineType.Electrical ? null : Values.RandomDouble(0.1, 99.9)
        };
    }
}