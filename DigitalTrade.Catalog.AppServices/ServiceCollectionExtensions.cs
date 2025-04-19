using DigitalTrade.Catalog.AppServices.Catalog;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalTrade.Catalog.AppServices;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        return services.AddScoped<ICatalogHandler, CatalogHandler>();
    }
}