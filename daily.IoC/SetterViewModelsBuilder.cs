using Microsoft.Extensions.DependencyInjection;

namespace daily.IoC
{
    public class SetterViewModelsBuilder
    {
        private Container _container;

        private SetterViewModelsBuilder() { }
        internal SetterViewModelsBuilder(Container container)
        {
            _container = container;
        }

        public SetterServicesBuilder SetViewModels(IEnumerable<Type> viewModelsTypes)
        {
            foreach (var viewModelType in viewModelsTypes)
            {
                _container.Services.AddTransient(viewModelType);
            }
            return new SetterServicesBuilder(_container);
        }
    }
}
