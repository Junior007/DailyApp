using Microsoft.Extensions.DependencyInjection;

namespace daily.IoC
{
    public class DependencyBuilder
    {
        private static Container _container;

        public static IServiceProvider ServiceProvider { get => _container.SrvcProvider; }
        public static SetterViewModelsBuilder SetMainView<T>() where T : class
        {
            ServiceCollection services = new ServiceCollection();
            services.AddScoped<T>();

            _container = new Container(services);

            return new SetterViewModelsBuilder(_container);
        }
    }
}