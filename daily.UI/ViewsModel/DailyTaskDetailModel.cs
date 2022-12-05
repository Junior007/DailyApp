using daily.application.Services;
using daily.domain.Models.Daily;
using System;

namespace daily.UI.ViewsModel
{
    internal class DailyTaskDetailModel : AbstractViewModel
    {
        public string Title
        {
            get => _dailyTask.Title;
            set
            {
                _dailyTask.Title = value;
                OnPropertyChanged();
            }
        }
        public string Description
        {
            get => _dailyTask.Description;
            set
            {
                _dailyTask.Description = value;
                OnPropertyChanged();
            }
        }
        public DailyTask DailyTask
        {
            private get => _dailyTask;
            set { _dailyTask = value; }
        }

        private DailyTask _dailyTask;

        private IDailyServices _dailyService;

        public DailyTaskDetailModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            _dailyTask = new DailyTask();
        }

    }
}
