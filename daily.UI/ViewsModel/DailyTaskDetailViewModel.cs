﻿using daily.application.Services;
using daily.domain.Models.Daily;
using daily.UI.Views.Controls;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Linq;

namespace daily.UI.ViewsModel
{
    internal class DailyTaskDetailViewModel : AbstractViewModel
    {
        private Guid _id;

        public Guid Id
        {
            get
            {
                if (_id == Guid.Empty)
                    _id = Guid.NewGuid();
                return _id;
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
        private StackPanel stackPanelContainer;
        private string Container = nameof(Container);

        private double _width;

        public DailyTaskDetailViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            base.OnLoaded(sender, e);

            if (SubTasks?.Count > 0)
            {

                FrameworkElement thisView = sender as FrameworkElement;
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