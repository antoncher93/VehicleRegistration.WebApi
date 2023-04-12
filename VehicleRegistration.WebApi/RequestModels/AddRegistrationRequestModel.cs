namespace VehicleRegistration.WebApi.RequestModels;

public class AddRegistrationRequestModel
{
   public int OwnerId { get; set; }
   public int VehicleId { get; set; }
   public string RegNumber { get; set; }
}