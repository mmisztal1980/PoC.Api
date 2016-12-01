using Microsoft.Extensions.DependencyInjection;

namespace PoC.ServiceDiscovery
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection UseServiceDiscovery(this IServiceCollection services)
        {
            return services;
        }
    }
}
