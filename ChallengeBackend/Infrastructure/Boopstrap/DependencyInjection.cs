using UnitTest.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Challenge.Infrastructure.Services;
using Challenge.Application.Interfaces;

namespace Challenge.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfiles).Assembly);

        services.AddTransient<IProducerService, ProducerService>();

        return services;
    }
}
