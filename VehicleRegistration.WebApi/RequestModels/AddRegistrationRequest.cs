using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.WebApi.RequestModels;

public class AddRegistrationRequest
{
   [Required]
   public int OwnerId { get; set; }
   
   [Required]
   public string Vin { get; set; }
   
   [Required]
   public int Region { get; set; }
}