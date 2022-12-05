using daily.Infrastructure;
using daily.IoC;
using Microsoft.Extensions.DependencyInjection;
using daily.UI.Views;
using System;
using System.Collections.Generic;
using System.Windows;

namespace daily.UI
{
    public partial class App : Application
    {
        //private ServiceProvider serviceProvider;

        public App()
        {
        }
        private void OnStartup(object sender, StartupEventArgs e)
        {
            IEnumerable<Type> viewTypes = GetViewModels.Types();
            ServiceProvider serviceProvider = DependencyBuilder
                .SetMainView<MainView>()
                .SetViewModels(viewTypes)
                .SetServices()
                .Build();

            //var mainWindow = serviceProvider.GetService<MainView>();
            //mainWindow.Show();


            var startWindow = serviceProvider.GetService<MainView>();
            startWindow.Show();

        }
    }
}
