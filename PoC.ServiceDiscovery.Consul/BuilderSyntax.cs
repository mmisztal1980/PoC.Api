using System;
using Consul;

namespace PoC.ServiceDiscovery.Consul
{
    public class BuilderSyntax : IBuilderSyntax, IServiceRegisteredSyntax
    {
        //private readonly IList<CatalogRegistration> registrations = new List<CatalogRegistration>();

        private readonly CatalogRegistration registration = new CatalogRegistration() { Service = new AgentService() };

        public BuilderSyntax()
        {
            
        }

        public IServiceDiscoveryClient Build()
        {
            var result = new ServiceDiscoveryClient();

            result
                .RegisterServiceAsync(registration)
                .Wait();

            return result;
        }

        public IServiceRegisteredSyntax RegisterApplication(string name)
        {
            registration.Node = "TSUNAMI";
            registration.Address = "localhost";
            registration.Service.ID = Guid.NewGuid().ToString();
            registration.Service.Service = name;

            return this;
        }

        public IServiceRegisteredSyntax WithAddress(string address)
        {
            var uri = new Uri(address);
            
            registration.Service.Address = string.IsNullOrEmpty(uri.UserInfo) ? 
                $"{uri.Scheme}://{uri.Host}:{uri.Port}" :
                $"{uri.Scheme}://{uri.UserInfo}@{uri.Host}:{uri.Port}";

            registration.Service.Port = uri.Port;            

            return this;
        }

        public IServiceRegisteredSyntax WithHttpCheck(string url)
        {
            

            return this;
        }

        public IServiceRegisteredSyntax WithProcessCheck()
        {
            return this;
        }

        public IServiceRegisteredSyntax WithDiskCheck()
        {
            return this;
        }

        public IServiceRegisteredSyntax WithCpuCheck()
        {
            return this;
        }

        public IServiceRegisteredSyntax WithTags(params string[] tags)
        {
            registration.Service.Tags = tags;

            return this;
        }
    }
}
