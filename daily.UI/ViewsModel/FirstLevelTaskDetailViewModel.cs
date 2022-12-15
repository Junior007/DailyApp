﻿using daily.application.Services;
using daily.UI.Views.Controls;
using System.Windows;
using System.Windows.Controls;

namespace daily.UI.ViewsModel
{
    internal class FirstLevelTaskDetailViewModel : AbstractDailyTaskDetailViewModel
    {


        public FirstLevelTaskDetailViewModel(IDailyServices dailyService) : base(dailyService)
        {

        }

        protected override void RefreshSubtaskViews(FrameworkElement? frameworkElement)
        {
            if (SubTasks?.Count > 0)
            {

                FrameworkElement thisView = frameworkElement as FrameworkElement;
                stackPanelContainer = thisView?.FindName(Container) as StackPanel;
                stackPanelContainer.Children.Clear();

                foreach (var task in SubTasks)
                {
                    SecondLevelTaskDetailView userControlDailyTaskDetail = new SecondLevelTaskDetailView();
                    SecondLevelTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as SecondLevelTaskDetailViewModel;
                    dailyTaskDetailModel.DailyTask = task;
                    stackPanelContainer.Children.Add(userControlDailyTaskDetail);
                }
            }
        }

     }
}
