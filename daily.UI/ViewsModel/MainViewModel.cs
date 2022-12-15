using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Commands;
using daily.UI.Views.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace daily.UI.ViewsModel
{
    internal class MainViewModel : AbstractViewModel
    {

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

        public MainViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            onSelectionChanged = new RelayCommand(changeDateTasksAction, value => true);

            onResizeCompleted.Enabled = true;
            onResizeCompleted.Elapsed += new ElapsedEventHandler(setSize);
        }


        private IDailyServices _dailyService;
        private StackPanel stackPanelContainer;
        private TabControl navBar;

        private string Container = nameof(Container);
        private string NavBar = nameof(NavBar);

        private double _containerWidth;
        private Timer onResizeCompleted = new Timer(250);


        private ICommand onSelectionChanged;

        private void changeDateTasksAction(object obj)
        {
            TabItem tab = (TabItem)navBar.SelectedItem;
            string lookfor = (string)tab.Header;

            AddSubtasks(OwnerView as FrameworkElement, lookfor);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            AddTabs(sender as FrameworkElement);

            TabItem tab = (TabItem)navBar.SelectedItem;
            string lookfor = (string)tab.Header;

            AddSubtasks(sender as FrameworkElement, lookfor);
        }

        protected override void OnResize(object sender, SizeChangedEventArgs e)
        {
            onResizeCompleted.Stop();
            base.OnResize(sender, e);
            onResizeCompleted.Start();
        }

        private void setSize(object? sender, ElapsedEventArgs e)
        {
            onResizeCompleted.Stop();
            ContainerWidth = ParentWidth - 40;
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

        private void AddSubtasks(FrameworkElement frameworkElement, string lookfor)
        {
            FrameworkElement thisView = frameworkElement as FrameworkElement;
            stackPanelContainer = thisView?.FindName(Container) as StackPanel;

            DateTime dateTime;
            DateTime.TryParse(lookfor, out dateTime);
            DailyTask mainTask = _dailyService.Get(dateTime);

            stackPanelContainer.Children.Clear();

            FirstLevelTaskDetailView userControlDailyTaskDetail = new FirstLevelTaskDetailView();
            FirstLevelTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as FirstLevelTaskDetailViewModel;
            dailyTaskDetailModel.DailyTask = mainTask;
            stackPanelContainer.Children.Add(userControlDailyTaskDetail);

        }
    }
}
