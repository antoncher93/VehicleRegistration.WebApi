using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Models;

public class Owner
{
    public int Id { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }
    
    public string PassportNumber { get; set; }
    
    public string PassportSeries { get; set; }

    [JsonIgnore]
    public ICollection<Registration>? Registrations { get; set; }
}