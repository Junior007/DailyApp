﻿using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Views.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;
using System.Windows.Input;
using daily.UI.Commands;
using System.Collections.Generic;
using System.Timers;

namespace daily.UI.ViewsModel
{
    internal class DailyTaskDetailViewModel : AbstractViewModel
    {

        public TimeSpan Timming
        {
            get => _timming;
            set
            {
                _timming = value;
                OnPropertyChanged();
            }
        }
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
        public bool IsRunning
        {
            get => _isRunning;
            set
            {
                _isRunning = value;
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

        public ICommand StartStop => startStop;

        private DailyTask _dailyTask;
        private string _title;
        private string _description;
        private bool _isRunning;
        private TimeSpan _timming = DateTime.Now - DateTime.Now;

        private IDailyServices _dailyService;
        private StackPanel stackPanelContainer;
        private string Container = nameof(Container);
        private ICommand startStop;

        private Timer refreshTimming = new Timer(30000);

        public DailyTaskDetailViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));
            startStop = new RelayCommand(startStopAction, value => true);


            refreshTimming.Enabled = true;
            refreshTimming.Elapsed += new ElapsedEventHandler((object? sender, ElapsedEventArgs e) => setTimming());

        }

        private void startStopAction(object obj)
        {
            if (DailyTask.IsRunning)
                DailyTask.Stop();
            else
                DailyTask.Start();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            AddSubtaskViews(sender as FrameworkElement);
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
            _dailyTask.TaskStartEvent += OnChangeTaskState;
            _dailyTask.TaskStopEvent += OnChangeTaskState;
        }

        private void OnChangeTaskState(object sender, Object e)
        {
            IsRunning = _dailyTask.IsRunning;
        }

        private void AddSubtaskViews(FrameworkElement? frameworkElement)
        {
            if (SubTasks?.Count > 0)
            {

                FrameworkElement thisView = frameworkElement as FrameworkElement;
                stackPanelContainer = thisView?.FindName(Container) as StackPanel;

                foreach (var task in SubTasks)
                {
                    DailyTaskDetailView userControlDailyTaskDetail = new DailyTaskDetailView();
                    DailyTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as DailyTaskDetailViewModel;
                    dailyTaskDetailModel.DailyTask = task;
                    stackPanelContainer.Children.Add(userControlDailyTaskDetail);
                }
            }
        }


        private void setTimming()
        {
            long ticks = _dailyTask.Intervals.Where(i => i.IsClose).Select(i => (TimeSpan)(i.End - i.Init)).Sum(ts=>ts.Ticks);
            if(_dailyTask.Intervals.Where(i => i.IsOpen).Any())
            {
                var lastOpen = _dailyTask.Intervals.Where(i => i.IsOpen).Last();
                ticks += (DateTime.Now - lastOpen.Init).Ticks;
            }
            Timming = new TimeSpan(ticks);
        }
    }
}
