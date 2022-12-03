using Microsoft.Extensions.DependencyInjection;

namespace daily.IoC
{
    public class DependencyBuilder
    {
        private static Container _container;

        public static IServiceProvider ServiceProvider { get => _container.SrvcProvider; }
        public static SetterViewsBuilder SetMainView<T>() where T : class
        {
            ServiceCollection services = new ServiceCollection();
            services.AddSingleton<T>();

            _container = new Container(services);

            return new SetterViewsBuilder(_container);
        }
    }
}