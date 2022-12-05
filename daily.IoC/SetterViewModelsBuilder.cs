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

        public SetterServicesBuilder SetViewModels(IEnumerable<Type> viewTypes)
        {
            foreach (var viewType in viewTypes)
            {
                _container.Services.AddSingleton(viewType);
            }
            return new SetterServicesBuilder(_container);
        }
    }
}
