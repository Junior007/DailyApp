using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Infrastructure;
using daily.UI.Views.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace daily.UI.ViewsModel
{
    internal class DailyTaskDetailViewModel : AbstractViewModel
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
        public ObservableCollection<DailyTask> SubTasks
        {
            get;
            set;
        }

        public DailyTask DailyTask
        {
            private get => _dailyTask;
            set
            {
                SetDailyTask(value);
            }
        }

        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        private DailyTask _dailyTask;
        private string _title;
        private string _description;

        private IDailyServices _dailyService;
        private ListView listViewContainer;
        private string ListViewContainer = nameof(ListViewContainer);

        private double _width;

        public DailyTaskDetailViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);

            if (DailyTask?.SubTasks.Count > 0)
            {

                FrameworkElement thisView = sender as FrameworkElement;
                listViewContainer = thisView?.FindName(ListViewContainer) as ListView;

                foreach (var task in DailyTask.SubTasks)
                {
                    DailyTaskDetailView dailyTaskDetailView = new DailyTaskDetailView();

                    DailyTaskDetailViewModel dailyTaskDetailViewModel = dailyTaskDetailView.DataContext as DailyTaskDetailViewModel;
                    dailyTaskDetailViewModel.DailyTask = task;

                    listViewContainer.Items.Add(dailyTaskDetailView);
                }
            }
        }
        protected override void OnResize(object sender, SizeChangedEventArgs e)
        {
            base.OnResize(sender, e);

            FrameworkElement thisView = sender as FrameworkElement;
            FrameworkElement parent = thisView.Parent as FrameworkElement;

            if (e.WidthChanged && parent != null)
            {
                Width = parent.ActualWidth;
            }
        }

        private void SetDailyTask(DailyTask value)
        {
            _dailyTask = value;
            _title = _dailyTask.Title;
            _description = _dailyTask.Description;
            SubTasks = new ObservableCollection<DailyTask>();
            foreach (var subTask in _dailyTask.SubTasks)
            {
                SubTasks.Add(subTask);
            }

            OnPropertyChanged(nameof(Title));
            OnPropertyChanged(nameof(Description));
        }
    }
}
