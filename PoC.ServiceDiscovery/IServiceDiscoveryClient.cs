using System;
using System.Threading.Tasks;

namespace PoC.ServiceDiscovery
{
    public interface IServiceDiscoveryClient : IDisposable
    {
        Task<string> GetAsync(string service);

        Task<string[]> GetAllAsync(string service);
    }
}
