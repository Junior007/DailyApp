using Microsoft.Extensions.DependencyInjection;

namespace daily.IoC
{
    internal class Container
    {
        internal ServiceProvider SrvcProvider;
        internal ServiceCollection Services;
        internal Container(ServiceCollection services)
        {
            Services = services;
        }
        internal void BuildServiceProvider()
        {
            SrvcProvider = Services.BuildServiceProvider();
        }
    }

}
