using daily.application.Service;
using daily.application.Services;
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
            _container.Services.AddSingleton<IDataServices, DataServices>();
            _container.Services.AddSingleton<IDailyServices, DailyServices>();

            return new Builder(_container); ;
        }


    }
}
