using Microsoft.Extensions.DependencyInjection;

namespace daily.IoC
{
    public class SetterViewsBuilder
    {
        private Container _container;

        private SetterViewsBuilder() { }
        internal SetterViewsBuilder(Container container)
        {
            _container = container;
        }

        public SetterServicesBuilder SetViews(IEnumerable<Type> viewTypes)
        {
            foreach (var viewType in viewTypes)
            {
                _container.Services.AddSingleton(viewType);
            }
            return new SetterServicesBuilder(_container);
        }
    }
}
