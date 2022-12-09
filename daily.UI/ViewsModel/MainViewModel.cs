using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Commands;
using daily.UI.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace daily.UI.ViewsModel
{
    internal class MainViewModel : AbstractViewModel
    {

        /*public DailyTask DailyWork
        {
            get => _dailyWork;
            set
            {
                _dailyWork = value;
                OnPropertyChanged();
            }
        }*/
        public ICommand OnSelectionChanged => onSelectionChanged;

        public double ContainerWidth
        {
            get => _containerWidth;
            private set
            {
                _containerWidth = value;
                OnPropertyChanged();
            }
        }

        //private DailyTask _dailyWork;
        private IDailyServices _dailyService;
        private StackPanel stackPanelContainer;
        private TabControl navBar;

        private string Container = nameof(Container);
        private string NavBar = nameof(NavBar);

        private double _containerWidth;


        private ICommand onSelectionChanged;

        public MainViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            onSelectionChanged = new RelayCommand(SelectionChanged, value => true);
        }

        private void SelectionChanged(object obj)
        {
            TabItem tab = (TabItem)navBar.SelectedItem;
            string lookfor =(string)tab.Header;

            AddSubtasks(OwnerView as FrameworkElement, lookfor);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            AddTabs(sender as FrameworkElement);
            AddSubtasks(sender as FrameworkElement);
        }

        protected override void OnResize(object sender, SizeChangedEventArgs e)
        {
            base.OnResize(sender, e);
            ContainerWidth = ParentWidth * 0.9;
        }

        private void AddTabs(FrameworkElement? frameworkElement)
        {
            FrameworkElement thisView = frameworkElement as FrameworkElement;
            navBar = thisView?.FindName(NavBar) as TabControl;

            IList<DailyTask> week = _dailyService.GetWeek();
            navBar.Items.Clear();

            foreach (var daily in week)
            {
                TabItem item = new TabItem
                {
                    Header = daily.Title
                };
                navBar.Items.Add(item);
            }
        }

        private void AddSubtasks(FrameworkElement? frameworkElement)
        {
            FrameworkElement thisView = frameworkElement as FrameworkElement;
            stackPanelContainer = thisView?.FindName(Container) as StackPanel;

            DailyTaskDetailView userControlDailyTaskDetail = new DailyTaskDetailView();
            DailyTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as DailyTaskDetailViewModel;
            dailyTaskDetailModel.DailyTask = _dailyService.Get();
            stackPanelContainer.Children.Clear();
            stackPanelContainer.Children.Add(userControlDailyTaskDetail);
        }

        private void AddSubtasks(FrameworkElement frameworkElement, string lookfor)
        {
            FrameworkElement thisView = frameworkElement as FrameworkElement;
            stackPanelContainer = thisView?.FindName(Container) as StackPanel;

            DailyTaskDetailView userControlDailyTaskDetail = new DailyTaskDetailView();
            DailyTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as DailyTaskDetailViewModel;
            DateTime dateTime;
            DateTime.TryParse( lookfor, out dateTime);
            dailyTaskDetailModel.DailyTask = _dailyService.Get(dateTime);
            stackPanelContainer.Children.Clear();
            stackPanelContainer.Children.Add(userControlDailyTaskDetail);
        }
    }
}
