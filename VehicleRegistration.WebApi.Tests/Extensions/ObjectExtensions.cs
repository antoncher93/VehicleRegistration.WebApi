namespace VehicleRegistration.WebApi.Tests.Extensions;

public static class ObjectExtensions
{
    public static List<T> AsList<T>(this T entity)
    {
        return new List<T>() { entity };
    }
}