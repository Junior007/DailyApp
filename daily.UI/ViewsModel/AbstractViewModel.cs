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

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected FrameworkElement _ownerView { get; set; }
        private double _width;

        internal FrameworkElement OwnerView
        {
            get => _ownerView;
            set
            {

                _ownerView = value;
                _ownerView.Loaded+=OnLoaded;
                _ownerView.Loaded += (sender, cancelArg) =>
                {

                    Window window = Window.GetWindow(_ownerView);
                    window.Closing += OnClose;

                };
                _ownerView.SizeChanged += OnResize;
                _ownerView.LostFocus += OnLostFocus;

            }
        }

        protected virtual void OnClose(object? sender, CancelEventArgs e)
        {
            
        }

        protected virtual void OnLostFocus(object sender, RoutedEventArgs e)
        {
        }

        protected virtual void OnResize(object sender, SizeChangedEventArgs e)
        {
            FrameworkElement parent = OwnerView.Parent as FrameworkElement;

            double parentWidth = parent != null? parent.ActualWidth :Application.Current.MainWindow.ActualWidth;

            if (e.WidthChanged)
            {
                ParentWidth = parentWidth;
            }
        }

        protected virtual void OnLoaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
