using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace wpf.learning.Infrastructure
{
    internal class GetViews
    {
        private static IEnumerable<Type> _views;

        public static IEnumerable<Type> Types()
        {
            if (_views == null)
            {
                var type = typeof(INotifyPropertyChanged);

                Assembly assembly = Assembly.GetExecutingAssembly();
                _views = assembly
                    .GetTypes()
                    .Where(types => types.Name != type.Name && types.IsAssignableTo(type));
            }
            return _views;
        }
    }
}
