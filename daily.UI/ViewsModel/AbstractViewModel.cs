using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace daily.UI.ViewsModel
{
    internal abstract class AbstractViewModel : INotifyPropertyChanged
    {

        protected FrameworkElement _ownerView { get; set; }
        internal FrameworkElement OwnerView
        {
            get => _ownerView;
            set
            {
                _ownerView = value;
                _ownerView.Loaded+=OnLoaded;
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
