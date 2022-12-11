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

        public double TextWidth {
            get => _textWidth;
            private set {
                _textWidth = value;
                OnPropertyChanged(); 
            } 
        }

        public ICommand Start => start;
        public ICommand Stop => stop;

        private DailyTask _dailyTask;
        private string _title;
        private string _description;
        private bool _isRunning;
        private double _textWidth;

        private IDailyServices _dailyService;
        private StackPanel stackPanelContainer;
        private string Container = nameof(Container);
        private ICommand start;
        private ICommand stop;

        public DailyTaskDetailViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            start = new RelayCommand(startAction, value => true);
            stop = new RelayCommand(stopAction, value => true);
        }

        private void startAction(object obj)
        {
            DailyTask.Start();
        }
        private void stopAction(object obj)
        {
            DailyTask.Stop();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);
            AddSubtasks(sender as FrameworkElement);
        }

        protected override void OnResize(object sender, SizeChangedEventArgs e)
        {
            base.OnResize(sender, e);
            TextWidth = ParentWidth * 0.9;
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
        private void AddSubtasks(FrameworkElement? frameworkElement)
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


    }
}
