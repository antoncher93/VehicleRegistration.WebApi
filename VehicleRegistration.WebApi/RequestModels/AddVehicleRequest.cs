using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.WebApi.RequestModels;

public class AddVehicleRequest
{
    [Required]
    public int ModelId { get; set; }
    
    [Required]
    public int EngineId { get; set; }
    
    [Required]
    public int BodyId { get; set; }
    
    [Required]
    public string Vin { get; set; }

    [Required]
    public string Color { get; set; }
}