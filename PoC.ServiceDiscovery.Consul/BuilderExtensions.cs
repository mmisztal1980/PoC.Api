namespace PoC.ServiceDiscovery.Consul
{
    public static class BuilderExtensions
    {
        public static IBuilderSyntax UseConsul(this IBuilder builder)
        {
            var syntax = new BuilderSyntax();

            builder.UseSyntax(syntax); 

            return syntax;            
        }
    }
}
