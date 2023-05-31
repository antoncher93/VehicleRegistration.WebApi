using VehicleRegistration.WebApi.Models;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Tests.Fakes;

public class FakeEngineTypeRepository : IEngineTypeRepository
{
    private readonly List<EngineType> _engineTypes = new();
    
    public Task<List<EngineType>> GetAllAsync()
    {
        return Task.FromResult(_engineTypes.ToList());
    }

    public Task<EngineType?> GetByIdAsync(int id)
    {
        var result = _engineTypes.FirstOrDefault(et => et.Id == id);
        
        return Task
            .FromResult(result);
    }

    public Task AddAsync(EngineType engineType)
    {
        _engineTypes.Add(engineType);
        return Task.CompletedTask;
    }

    public void SetupManyEngineTypes(IEnumerable<EngineType> engineTypes)
    {
        _engineTypes.AddRange(engineTypes);
    }
}