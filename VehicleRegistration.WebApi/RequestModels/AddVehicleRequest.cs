using System.ComponentModel.DataAnnotations;
using VehicleRegistration.WebApi.Types;

namespace VehicleRegistration.WebApi.RequestModels;

public class AddVehicleRequest
{
    [Required]
    public int ModelId { get; set; }
    
    [Required]
    public int EngineTypeId { get; set; }
    
    [Required]
    public int BodyId { get; set; }
    
    [Required]
    public string Vin { get; set; }

    [Required]
    public string Color { get; set; }

    [Required]
    public string EngineNumber { get; set; }

    [Required]
    public double HorsePower { get; set; }

    [Required]
    public double? Volume { get; set; }

    [Required]
    public int Transmission { get; set; }
}