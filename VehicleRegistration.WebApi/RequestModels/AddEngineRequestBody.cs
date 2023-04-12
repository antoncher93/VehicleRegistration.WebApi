namespace VehicleRegistration.WebApi.RequestModels;

public class AddEngineRequestBody
{
    public Types.EngineType EngineType { get; set; }
    public double HorsePower { get; set; }
    public double? Volume { get; set; }
}