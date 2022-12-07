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
        private StackPanel stackPanelContainer;
        private string Container = nameof(Container);

        public MainViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            _addCommand = new AddCommand();
            _deleteCommand = new DeleteCommand();
            _startCommand = new StartCommand();
            _stopCommand = new StopCommand();

            DailyWork = _dailyService.Get();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            DailyTaskDetail dailyTaskDetail = new DailyTaskDetail();
            FrameworkElement view = sender as FrameworkElement;
            stackPanelContainer = view?.FindName(Container) as StackPanel;
            stackPanelContainer.Children.Clear();
            stackPanelContainer.Children.Add(dailyTaskDetail);

            DailyTaskDetailModel ucModelView = dailyTaskDetail.DataContext as DailyTaskDetailModel;

            ucModelView.DailyTask = _dailyService.Get();
        }
    }
}
