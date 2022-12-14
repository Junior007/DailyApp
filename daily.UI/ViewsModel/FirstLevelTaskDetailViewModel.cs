using daily.application.Services;
using daily.UI.Commands;
using daily.UI.Views.Controls;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace daily.UI.ViewsModel
{
    internal class FirstLevelTaskDetailViewModel : AbstractDailyTaskDetailViewModel
    {

        public ICommand OnButtonClickSave => onButtonClickSave;
        private ICommand onButtonClickSave;
        public FirstLevelTaskDetailViewModel(IDailyServices dailyService) : base(dailyService)
        {
            onButtonClickSave = new RelayCommand(SaveAction, value => true);
        }

        private void SaveAction(object obj)
        {
            _dailyService.Save(DailyTask);
        }
        protected override void OnLostFocus(object sender, RoutedEventArgs e)
        {
            base.OnLostFocus(sender, e);
            SaveAction(sender);
        }


        protected override void RefreshSubtasksViews()
        {
            stackPanelContainer = this.OwnerView?.FindName(Container) as WrapPanel;
            stackPanelContainer?.Children.Clear();

            if (SubTasks?.Count > 0)
            {
                foreach (var task in SubTasks)
                {
                    SecondLevelTaskDetailView userControlDailyTaskDetail = new SecondLevelTaskDetailView();
                    SecondLevelTaskDetailViewModel dailyTaskDetailModel = userControlDailyTaskDetail.DataContext as SecondLevelTaskDetailViewModel;
                    dailyTaskDetailModel.DailyTask = task;
                    stackPanelContainer.Children.Add(userControlDailyTaskDetail);

                    dailyTaskDetailModel.DeleteTaskEvent += (sender, id) => DeleteSubTask(id);
                }
            }
        }

     }
}
