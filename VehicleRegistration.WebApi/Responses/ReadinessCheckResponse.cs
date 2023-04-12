namespace VehicleRegistration.WebApi.Responses;

public class ReadinessCheckResponse
{
    public bool IsAvailable { get; set; }

    public static ReadinessCheckResponse FromOk()
    {
        return new ReadinessCheckResponse()
        {
            IsAvailable = true,
        };
    }
}