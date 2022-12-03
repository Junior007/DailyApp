using daily.application.Services;
using daily.domain.Models.Daily;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace daily.UI.ViewsModel.Journey
{
    internal class JourneyViewModel : INotifyPropertyChanged
    {
        private IDailyServices _dailyService;

        public ICommand _addCommand;
        public ICommand AddCommand { get => _addCommand; }
        public ICommand _deleteCommand;
        public ICommand DeleteCommand { get => _deleteCommand; }

        public JourneyViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            _addCommand = new AddCommand();
            _deleteCommand = new DeleteCommand();

            _dataDetail = _dailyService.Get();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DailyTask _dataDetail;
        public DailyTask DataDetail
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
