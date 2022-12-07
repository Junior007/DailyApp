using daily.application.Services;
using daily.domain.Models.Daily;
using System;

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
                _dailyTask.Title = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                _dailyTask.Description = value;
                OnPropertyChanged();
            }
        }
        public DailyTask DailyTask
        {
            private get => _dailyTask;
            set {
                SetDailyTask(value);
            }
        }

        private void SetDailyTask(DailyTask value)
        {
            _dailyTask = value;
            _title = _dailyTask.Title;
            _description = _dailyTask.Description;
            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));

        }

        private DailyTask _dailyTask;
        private string _title;
        private string _description;

        private IDailyServices _dailyService;

        public DailyTaskDetailModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

       }

    }
}
