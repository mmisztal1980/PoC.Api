using Xunit;

namespace PoC.ServiceDiscovery.Consul.IntegrationTests
{
    public class GivenAServiceDiscoveryBuilder
    {
        [Theory]
        [InlineData("test-app1", "http://localhost:80")]
        [InlineData("test-app2", "https://localhost:5000")]
        [InlineData("test-app3", "akka.tcp://system@localhost:5000")]
        public void WhenApplicationIsRegistered_ThenItIsVisibleInConsulCatalog(string appName, string address)
        {
            var builder = new BuilderSyntax()
                    .RegisterApplication(appName)
                    .WithAddress(address)
                    .WithTags("api", "test");

            using (var client = builder.Build())
            {


                var serviceAddress = client.GetAsync(appName).Result;

                Assert.Equal(address, serviceAddress);
            }
        }

        [Fact]
        public void WhenApplicationIsRegisteredMultipleTimes_ThenAllAddressesAreVisibleInServiceDiscovery()
        {
            const string appName = "test-service";
            const int count = 10;

            var clients = new IServiceDiscoveryClient[count];

            for (var i = 0; i < count; i++)
            {
                var builder = new BuilderSyntax()
                    .RegisterApplication(appName)
                    .WithAddress($"https://localhost:{5000 + i}")
                    .WithTags("api", "test");


                clients[i] = builder.Build();
            }

            var result = clients[0].GetAllAsync(appName).Result;

            Assert.Equal(count, result.Length);

            for (var i = 0; i < count; i++)
            {
                clients[i].Dispose();
            }
        }
    }
}
