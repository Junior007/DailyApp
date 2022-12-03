using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace daily.Infrastructure
{
    internal class GetViews
    {
        private static IEnumerable<Type> _views;

        public static IEnumerable<Type> Types()
        {
            if (_views == null)
            {
                var type = typeof(INotifyPropertyChanged);

                Assembly assembly = Assembly.GetAssembly(typeof(GetViews));
                _views = assembly
                    .GetTypes()
                    .Where(types => types.Name != type.Name && types.IsAssignableTo(type));
            }
            return _views;
        }
    }
}
