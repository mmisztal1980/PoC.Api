using System.Threading.Tasks;

namespace PoC.ServiceDiscovery
{
    public interface IKeyValueStore
    {
        Task<T> GetAsync<T>(string key);
    }
}
