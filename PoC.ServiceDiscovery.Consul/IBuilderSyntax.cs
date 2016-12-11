namespace PoC.ServiceDiscovery.Consul
{
    public interface IBuilderSyntax : ISyntax
    {
        IServiceRegisteredSyntax RegisterApplication(string name);
    }

    public interface IServiceRegisteredSyntax : ISyntax
    {
        IServiceRegisteredSyntax WithAddress(string address);
        IServiceRegisteredSyntax WithHttpCheck(string url);
        IServiceRegisteredSyntax WithProcessCheck();
        IServiceRegisteredSyntax WithDiskCheck();
        IServiceRegisteredSyntax WithCpuCheck();

        IServiceRegisteredSyntax WithTags(params string[] tags);
    }
}
