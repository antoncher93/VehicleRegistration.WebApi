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

    public static Model RandomModel(int brandId)
    {
        return new Model()
        {
            BrandId = brandId,
            ModelName = Values.RandomString(),
        };
    }
}