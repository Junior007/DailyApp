using daily.learning.application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace daily.IoC
{
    public class SetterServicesBuilder
    {
        private Container _container;

        private SetterServicesBuilder(){}
        internal SetterServicesBuilder(Container container)
        {
            _container = container;
        }

        public Builder SetServices()
        {
            _container.Services.AddSingleton<IDataService, DataService>();

            return new Builder(_container); ;
        }


    }
}
