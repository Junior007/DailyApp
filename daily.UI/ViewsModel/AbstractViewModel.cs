using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace daily.UI.ViewsModel
{
    internal abstract class AbstractViewModel : INotifyPropertyChanged
    {
        internal FrameworkElement OwnerView { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
