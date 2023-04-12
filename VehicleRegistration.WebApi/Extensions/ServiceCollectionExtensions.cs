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
        return serviceCollection;
    }
}