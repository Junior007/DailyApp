using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wpf.learning.application.Service;

namespace wpf.learning.IoC
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
