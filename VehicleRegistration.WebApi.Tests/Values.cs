namespace VehicleRegistration.WebApi.Tests;

public static class Values
{
    public static string RandomString() => Guid.NewGuid().ToString();

    public static int RandomInt()
    {
        return new Random().Next(0, Int32.MaxValue);
    }

    public static List<T> ListOf<T>(Func<T> value)
    {
        var list = new List<T>();

        for (int i = 0; i < 5; i++)
        {
            list.Add(value());
        }

        return list;
    }

    public static double RandomDouble(double minValue, double maxValue)
    {
        return new Random().NextDouble() * (maxValue - minValue) + minValue;
    }
}