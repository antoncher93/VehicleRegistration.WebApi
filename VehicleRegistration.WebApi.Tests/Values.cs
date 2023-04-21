namespace VehicleRegistration.WebApi.Tests;

public static class Values
{
    public static string RandomString() => Guid.NewGuid().ToString();

    public static int RandomInt()
    {
        return new Random().Next(0, Int32.MaxValue);
    }
    
    public static int RandomInt(int minValue, int maxValue)
    {
        return new Random().Next(minValue, maxValue);
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
    
    public static double RandomDouble()
    {
        return new Random().NextDouble();
    }

    public static double RandomDouble(double minValue, double maxValue)
    {
        return new Random().NextDouble() * (maxValue - minValue) + minValue;
    }

    public static T RandomEnum<T>() where T : struct, Enum
    {
        return Enum
            .GetValues<T>()
            .MinBy(_ => Values.RandomInt());
    }
}