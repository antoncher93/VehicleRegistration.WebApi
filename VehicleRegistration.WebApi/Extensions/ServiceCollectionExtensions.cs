using VehicleRegistration.WebApi.Infrastructure;
using VehicleRegistration.WebApi.Repositories;

namespace VehicleRegistration.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(
        this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IBrandRepository, BrandRepository>();
        serviceCollection.AddScoped<IEngineRepository, EngineRepository>();
        serviceCollection.AddScoped<IModelRepository, ModelRepository>();
        serviceCollection.AddScoped<IBodyRepository, BodyRepository>();
        serviceCollection.AddScoped<IVehicleRepository, VehicleRepository>();
        serviceCollection.AddScoped<IOwnerRepository, OwnerRepository>();
        serviceCollection.AddScoped<IRegistrationRepository, RegistrationRepository>();
        serviceCollection.AddScoped<IEngineTypeRepository, EngineTypeRepository>();
        return serviceCollection;
    }
}