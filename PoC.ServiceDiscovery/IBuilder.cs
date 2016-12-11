namespace PoC.ServiceDiscovery
{
    public interface IBuilder
    {
        void UseSyntax(ISyntax syntax);
        IServiceDiscoveryClient Build();
    }
}
