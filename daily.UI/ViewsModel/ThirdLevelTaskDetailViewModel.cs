using daily.application.Services;
using daily.UI.Views.Controls;
using System.Windows;
using System.Windows.Controls;

namespace daily.UI.ViewsModel
{
    internal class ThirdLevelTaskDetailViewModel : AbstractDailyTaskDetailViewModel
    {


        public ThirdLevelTaskDetailViewModel(IDailyServices dailyService) : base(dailyService)
        {

        }

        protected override void AddSubtaskViews(FrameworkElement? frameworkElement)
        {
            if (SubTasks?.Count > 0)
            {

                FrameworkElement thisView = frameworkElement as FrameworkElement;
                stackPanelContainer = thisView?.FindName(Container) as StackPanel;

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
