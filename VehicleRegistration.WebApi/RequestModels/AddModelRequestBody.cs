using System.ComponentModel.DataAnnotations;

namespace VehicleRegistration.WebApi.RequestModels;

public class AddModelRequestBody
{
    [Required]
    public string Name { get; set; }
    
    public List<int>? EngineIds { get; set; }
    
    public List<int>? BodyIds { get; set; }
}