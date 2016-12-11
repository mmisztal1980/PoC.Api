using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Consul;

namespace PoC.ServiceDiscovery.Consul
{
    public class ServiceDiscoveryClient : IServiceDiscoveryClient
    {
        private const int TimeOut = 5000;
        private readonly ConsulClient client;
        private CatalogDeregistration deregistration;

        public ServiceDiscoveryClient()
        {
            client = new ConsulClient();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public async Task<string[]> GetAllAsync(string service)
        {
            return (await client.Catalog.Service(service, null, QueryOptions.Default))
                .Response
                .Select(x => x.ServiceAddress).ToArray();
        }

        public async Task<string> GetAsync(string service)
        {
            var services = await client.Catalog.Service(service, null, QueryOptions.Default);

            var svc = services.Response.Random();

            return svc.ServiceAddress;
        }

        ~ServiceDiscoveryClient()
        {
            Dispose(false);
        }

        public async Task<bool> DeregisterServiceAsync(CatalogDeregistration registration)
        {
            var task = client.Catalog.Deregister(registration);

            return await ExecuteTaskAsync(task, TimeOut);
        }

        public async Task<bool> RegisterServiceAsync(CatalogRegistration registration)
        {
            deregistration = registration.ToDeregistration();

            var task = client.Catalog.Register(registration);

            return await ExecuteTaskAsync(task, TimeOut);
        }

        public async Task<bool> RegisterServiceAsync(CatalogRegistration registration, CancellationToken token)
        {
            var task = client.Catalog.Register(registration, token);

            return await ExecuteTaskAsync(task, TimeOut);
        }

        private void Dispose(bool disposing)
        {
            DeregisterServiceAsync(deregistration).Wait();

            if (disposing)
            {
                client?.Dispose();
            }
        }

        private async Task<bool> ExecuteTaskAsync(Task task, int timeout)
        {
            return await Task.WhenAny(task, Task.Delay(timeout)) == task;
        }
    }
}
