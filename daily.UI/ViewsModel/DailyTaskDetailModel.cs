using daily.application.Services;
using daily.domain.Models.Daily;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace daily.UI.ViewsModel
{
    internal class DailyTaskDetailModel : AbstractViewModel
    {

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddCommand { get => _addCommand; }
        public ICommand DeleteCommand { get => _deleteCommand; }
        public ICommand StartCommand { get => _startCommand; }
        public ICommand StopCommand { get => _stopCommand; }

        private ICommand _addCommand;
        private ICommand _deleteCommand;
        private ICommand _startCommand;
        private ICommand _stopCommand;

        private string _title;
        private string _description;

        private IDailyServices _dailyService;

        public DailyTaskDetailModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            DailyTask daily = _dailyService.Get();
            Title = daily.Title;
            Description = daily.Description;
        }

        protected override void OnSetOwnerView()
        {
            var chekOwnerView = OwnerView;
        }
    }
}
