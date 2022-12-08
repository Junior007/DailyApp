﻿using daily.application.Services;
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


        public double Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        private DailyTask _dailyWork;
        private IDailyServices _dailyService;
        private StackPanel stackPanelContainer;
        private string Container = nameof(Container);
        private double _width;

        public MainViewModel(IDailyServices dailyService)
        {
            _dailyService = dailyService ?? throw new ArgumentNullException(nameof(dailyService));

            DailyWork = _dailyService.Get();
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement thisView = sender as FrameworkElement;
            stackPanelContainer = thisView?.FindName(Container) as StackPanel;


            DailyTaskDetailView userControlDailyTaskDetail = new DailyTaskDetailView();
            DailyTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as DailyTaskDetailViewModel;
            dailyTaskDetailModel.DailyTask = _dailyService.Get();

            stackPanelContainer.Children.Add(userControlDailyTaskDetail);
        }

        protected override void OnResize(object sender, SizeChangedEventArgs e)
        {
            base.OnResize(sender, e);

            if (e.WidthChanged)
            {
                FrameworkElement thisView = sender as FrameworkElement;
                Width = thisView.ActualWidth;
            }
        }

    }
}
