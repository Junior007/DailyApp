using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace daily.UI.ViewsModel
{
    internal abstract class AbstractViewModel : INotifyPropertyChanged
    {

        public double ParentWidth
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }
        private double _width;

        protected FrameworkElement _ownerView { get; set; }
        internal FrameworkElement OwnerView
        {
            get => _ownerView;
            set
            {
                _ownerView = value;
                _ownerView.Loaded+=OnLoaded;
                _ownerView.SizeChanged += OnResize;
            }
        }

        protected virtual void OnResize(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement thisView = sender as FrameworkElement;
            FrameworkElement parent = thisView.Parent as FrameworkElement;

            if (e.WidthChanged && parent != null)
            {
                ParentWidth = parent.ActualWidth;
            }
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }


        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
