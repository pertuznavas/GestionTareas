using GestionTareasApi.Contracts;
using GestionTareasApi.Services;

namespace GestionTareasApi.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}
