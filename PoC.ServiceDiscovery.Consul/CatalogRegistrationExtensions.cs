using Consul;

namespace PoC.ServiceDiscovery.Consul
{
    public static class CatalogRegistrationExtensions
    {
        public static CatalogDeregistration ToDeregistration(this CatalogRegistration registration)
        {
            var result = new CatalogDeregistration()
            {
                
                ServiceID = registration.Service.ID,
                Datacenter = registration.Datacenter,
                Node = registration.Node
            };

            return result;
        }
    }
}
