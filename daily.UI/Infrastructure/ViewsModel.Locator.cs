using System;
using System.Linq;
using System.Windows;
using daily.Infrastructure;
using daily.IoC;
using daily.UI.ViewsModel;

namespace daily.UI.Infrastructure
{
    internal static class Locator
    {
       
        public static bool GetIsAutomaticLocator(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsAutomaticLocatorProperty);
        }
        public static void SetIsAutomaticLocator(DependencyObject obj, bool value)
        {
            obj.SetValue(IsAutomaticLocatorProperty, value);
        }


        public static readonly DependencyProperty IsAutomaticLocatorProperty = DependencyProperty.RegisterAttached("IsAutomaticLocator", typeof(bool), typeof(Locator), new PropertyMetadata(false, IsAutomaticLocatorChanged));
        private static void IsAutomaticLocatorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement ownerView = d as FrameworkElement;
            string viewModelClassName = GetViewModelClassName(d);
            AbstractViewModel viewModel = GetInstanceOf(ownerView.GetType(), viewModelClassName);
            viewModel.OwnerView = ownerView;
            ownerView.DataContext = viewModel;
        }
        public static string GetViewModelClassName(DependencyObject obj)
        {
            return  (string)obj.GetValue(ViewModelClassNameProperty);
        }
        public static void SetViewModelClassName(DependencyObject obj, string value)
        {
            obj.SetValue(ViewModelClassNameProperty, value);
        }
        public static readonly DependencyProperty ViewModelClassNameProperty = DependencyProperty.RegisterAttached("ViewModelClassName", typeof(string), typeof(Locator), new PropertyMetadata(null));

        private static AbstractViewModel GetInstanceOf(Type dependencyPropertyType, string className)
        {
            var viewModelName = GetClassName(dependencyPropertyType, className);

            Type? viewModel = GetViews.Types().FirstOrDefault(t => t.Name == viewModelName);

            AbstractViewModel result = DependencyBuilder.ServiceProvider.GetService(viewModel) as AbstractViewModel;
            return result;
        }

        private static string GetClassName(Type dependencyPropertyType, string className)
        {
            if (string.IsNullOrWhiteSpace(className)) return $"{dependencyPropertyType.Name}Model";

            return className;
        }
        
    }
}
