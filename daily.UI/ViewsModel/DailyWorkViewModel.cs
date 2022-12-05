using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Commands;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace daily.UI.ViewsModel
{
    internal class DailyWorkViewModel : INotifyPropertyChanged
    {
        public DailyTask DailyWork
        {
            get => _dailyWork;
            set
            {
                _dailyWork = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get => _addCommand; }
        public ICommand DeleteCommand { get => _deleteCommand; }
        public ICommand StartCommand { get => _startCommand; }
        public ICommand StopCommand { get => _stopCommand; }

        private DailyTask _dailyWork;

        private ICommand _addCommand;
        private ICommand _deleteCommand;
        private ICommand _startCommand;
        private ICommand _stopCommand;

        private IDailyServices _dailyService;

        public DailyWorkViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            _addCommand = new AddCommand();
            _deleteCommand = new DeleteCommand();
            _startCommand = new StartCommand();
            _stopCommand = new StopCommand();

            DailyWork = _dailyService.Get();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
