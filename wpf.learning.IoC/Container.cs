using Microsoft.Extensions.DependencyInjection;

namespace wpf.learning.IoC
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
