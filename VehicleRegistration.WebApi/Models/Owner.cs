using Newtonsoft.Json;

namespace VehicleRegistration.WebApi.Models;

public class Owner
{
    public Owner()
    {
    }
    
    public Owner(
        string firstName,
        string lastName,
        string middleName)
    {
        FirstName = firstName;
        LastName = lastName;
        MiddleName = middleName;
    }
    public int Id { get; set; }
    
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string MiddleName { get; set; }
    
    public int PassportNumber { get; set; }
    
    public int PassportSeries { get; set; }

    [JsonIgnore]
    public ICollection<Registration>? Registrations { get; set; }
}