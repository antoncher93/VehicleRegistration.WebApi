using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.WebApi.RequestModels;

public class AddModelRequest
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public int BrandId { get; set; }
    
    public List<int>? EngineIds { get; set; }
    
    public List<int>? BodyIds { get; set; }
}