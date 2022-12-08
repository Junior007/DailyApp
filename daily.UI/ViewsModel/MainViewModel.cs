using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Commands;
using daily.UI.Views.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace daily.UI.ViewsModel
{
    internal class MainViewModel : AbstractViewModel
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
        public ICommand OnClickTab => onClickTab;

        public double ContainerWidth
        {
            get=> _containerWidth; 
            private set
            {
                _containerWidth = value;
                OnPropertyChanged();
            }
        }

        private DailyTask _dailyWork;
        private IDailyServices _dailyService;
        private StackPanel stackPanelContainer;
        private string Container = nameof(Container);
        private double _containerWidth;


        private ICommand onClickTab;

        public MainViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            DailyWork = _dailyService.Get();

            onClickTab = new RelayCommand(ClickTab, value=>true);

        }

        private void ClickTab(object obj)
        {
            //throw new NotImplementedException();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            AddSubtasks(sender as FrameworkElement);
        }

        protected override void OnResize(object sender, SizeChangedEventArgs e)
        {
            base.OnResize(sender, e);
            ContainerWidth = ParentWidth * 0.9;
        }

        private void AddSubtasks(FrameworkElement? frameworkElement)
        {
            FrameworkElement thisView = frameworkElement as FrameworkElement;
            stackPanelContainer = thisView?.FindName(Container) as StackPanel;

            DailyTaskDetailView userControlDailyTaskDetail = new DailyTaskDetailView();
            DailyTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as DailyTaskDetailViewModel;
            dailyTaskDetailModel.DailyTask = _dailyService.Get();

            stackPanelContainer.Children.Add(userControlDailyTaskDetail);
        }

    }
}
