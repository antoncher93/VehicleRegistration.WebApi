namespace VehicleRegistration.WebApi.RequestModels;

public class GetOwnerByFullNameRequest
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string MiddleName { get; set; }
}