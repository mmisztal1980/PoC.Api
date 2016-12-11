namespace PoC.ServiceDiscovery
{
    internal class Builder : IBuilder
    {
        private ISyntax syntax;

        public void UseSyntax(ISyntax syntax)
        {
            this.syntax = syntax;
        }

        public IServiceDiscoveryClient Build()
        {
            return syntax.Build();
        }
    }
}
