namespace VehicleRegistration.WebApi.RequestModels;

public class UpdateModelRequestBody
{
    public List<int>? EngineIds { get; set; }
    public List<int>? BodyIds { get; set; }
}