using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Commands;
using daily.UI.Views.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace daily.UI.ViewsModel
{
    internal class MainViewModel : AbstractViewModel
    {

        private string _dateTimeFormat = "dd-MM-yyyy";
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

        private IDailyServices _dailyService;
        private TabControl navBar;

        private string Container = nameof(Container);
        private string NavBar = nameof(NavBar);

        private double _containerWidth;
        private Timer onResizeCompletedLapTimmer = new Timer(250);

        private ICommand onSelectionChanged;

        public MainViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            onSelectionChanged = new RelayCommand(changeTabAction, value => true);

            onResizeCompletedLapTimmer.Enabled = true;
            onResizeCompletedLapTimmer.Elapsed += new ElapsedEventHandler(SetSize);
        }

        private void changeTabAction(object obj)
        {

            TabItem tab = (TabItem)navBar.SelectedItem;

            SetSelectedTask(tab);
        }

        protected override void OnClose(object? sender, CancelEventArgs e)
        {
            base.OnClose(sender, e);
        }
        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            AddTabs();

            if (navBar?.Items[0] != null)
            {
                SetSelectedTask(navBar.Items[0] as TabItem);
            }
        }

        protected override void OnResize(object sender, SizeChangedEventArgs e)
        {
            base.OnResize(sender, e);
            onResizeCompletedLapTimmer.Stop();
            onResizeCompletedLapTimmer.Start();
        }

        private void SetSize(object? sender, ElapsedEventArgs e)
        {
            onResizeCompletedLapTimmer.Stop();
            ContainerWidth = ParentWidth - 40;
        }

        private void SetSelectedTask(TabItem selectedTab)
        {
            navBar.SelectedItem = selectedTab;
            string lookfor = (string)selectedTab.Header;
        }
        private void AddTabs()
        {
            navBar = this.OwnerView?.FindName(NavBar) as TabControl;

            IList<DailyTask> week = _dailyService.GetWeek();
            navBar.Items.Clear();

            foreach (var daily in week)
            {
                TabItem item = new TabItem
                {
                    Header = daily.Date.ToString(_dateTimeFormat),
                    Content = GetSubtaskTemplate(daily.Date)

                };
                navBar.Items.Add(item);
            }
        }

        private FirstLevelTaskDetailView GetSubtaskTemplate(DateTime dateTime)
        {
            FirstLevelTaskDetailView userControlDailyTaskDetail = new FirstLevelTaskDetailView();
            FirstLevelTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as FirstLevelTaskDetailViewModel;
            dailyTaskDetailModel.DailyTask = _dailyService.Get(dateTime);
            return userControlDailyTaskDetail;

        }

        private DailyTask LookFor(string lookfor)
        {

            DateTime dateTime;
            DateTime.TryParseExact(lookfor,
                       _dateTimeFormat,
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out dateTime);

            return _dailyService.Get(dateTime);
        }
    }
}
