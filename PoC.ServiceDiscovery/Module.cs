using System;
using Microsoft.Extensions.DependencyInjection;

namespace PoC.ServiceDiscovery
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, Action<IBuilder> serviceDiscovery)
        {
            var builder = new Builder();

            serviceDiscovery?.Invoke(builder);

            services.AddSingleton(builder.Build());

            return services;
        }
    }
}
