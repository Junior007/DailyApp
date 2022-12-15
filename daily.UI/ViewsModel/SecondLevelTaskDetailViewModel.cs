using daily.application.Services;
using daily.UI.Views.Controls;
using System.Windows;
using System.Windows.Controls;

namespace daily.UI.ViewsModel
{
    internal class SecondLevelTaskDetailViewModel : AbstractDailyTaskDetailViewModel
    {


        public SecondLevelTaskDetailViewModel(IDailyServices dailyService) : base(dailyService)
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
                    ThirdLevelTaskDetailView userControlDailyTaskDetail = new ThirdLevelTaskDetailView();
                    ThirdLevelTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as ThirdLevelTaskDetailViewModel;
                    dailyTaskDetailModel.DailyTask = task;
                    stackPanelContainer.Children.Add(userControlDailyTaskDetail);
                }
            }
        }

     }
}
