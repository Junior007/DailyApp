using daily.application.Services;
using daily.domain.Models.Daily;
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

        protected override void RefreshSubtasksViews()
        {
            stackPanelContainer = this.OwnerView?.FindName(Container) as WrapPanel;
            stackPanelContainer?.Children.Clear();

            if (SubTasks?.Count > 0)
            {

                foreach (var task in SubTasks)
                {
                    ThirdLevelTaskDetailView userControlDailyTaskDetail = new ThirdLevelTaskDetailView();
                    ThirdLevelTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as ThirdLevelTaskDetailViewModel;
                    dailyTaskDetailModel.DailyTask = task;
                    stackPanelContainer.Children.Add(userControlDailyTaskDetail);

                    dailyTaskDetailModel.DeleteTaskEvent+= (sender, id) => DeleteSubTask(id);
                }
            }
        }

     }
}
