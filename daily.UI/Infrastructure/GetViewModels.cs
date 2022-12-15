using daily.UI.ViewsModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace daily.Infrastructure
{
    internal class GetViewModels
    {
        private static IEnumerable<Type> _views;

        public static IEnumerable<Type> Types()
        {
            if (_views == null)
            {
                var baseType = typeof(AbstractViewModel);

                Assembly assembly = Assembly.GetAssembly(typeof(GetViewModels));
                _views = assembly
                    .GetTypes()
                    .Where(modelViewType => modelViewType.IsAssignableTo(baseType) && !modelViewType.IsAbstract);
            }
            return _views;
        }
    }
}
