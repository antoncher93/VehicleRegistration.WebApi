using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.WebApi.RequestModels;

public class AddModelRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int BrandId { get; set; }
}