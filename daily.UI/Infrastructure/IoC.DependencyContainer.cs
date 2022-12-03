using Microsoft.Extensions.DependencyInjection;
using MyWpfProject.Services;
using MyWpfProject.Views;
using MyWpfProject.ViewsModel;
using System.ComponentModel;

namespace MyWpfProject.Infrastructure.IoC
{
    internal class DependencyContainer
    {
        private static ServiceProvider _serviceProvider;

        internal static ServiceProvider ServiceProvider()
        {
            if (_serviceProvider == null)
            {
                ServiceCollection services = RegisterServices();
                _serviceProvider = services.BuildServiceProvider();
            }
            return _serviceProvider;
        }

        private static ServiceCollection RegisterServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton<IDataService, DataService>();
            services.AddSingleton<MainView>();

            //ViewModel
            var nameViews = GetViews.Types();
            foreach(var nameView in nameViews)
            {
                services.AddSingleton(nameView);
            }
            

            return services;

        }

    }
}
