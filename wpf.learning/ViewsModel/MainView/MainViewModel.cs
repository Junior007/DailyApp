using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf.learning.application.Models;
using wpf.learning.application.Service;

namespace MyWpfProject.ViewsModel.MainView
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IDataService _dataService;
        public int MyProperty { get; set; }

        public ICommand _addCommand;
        public ICommand AddCommand  { get => _addCommand; }
        public ICommand _deleteCommand;
        public ICommand DeleteCommand { get => _deleteCommand; }

        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));

            _addCommand = new AddCommand();
            _deleteCommand = new DeleteCommand();

            DataDetails = new ObservableCollection<DataDetail>(_dataService.GetDataDetails());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<DataDetail> DataDetails { get; set; }

        private DataDetail _dataDetail;
        public DataDetail DataDetail
        {
            get => _dataDetail;
            set
            {
                _dataDetail = value;
                OnPropertyChanged();
            }
        }



    }
}
