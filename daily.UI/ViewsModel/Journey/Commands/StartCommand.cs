using System;
using System.Windows;
using System.Windows.Input;

namespace daily.UI.ViewsModel.DailyWork.Commands
{
    public class StartCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            //DataDetail data = parameter as DataDetail;
            MessageBoxResult result = MessageBox.Show("StartCommand Hello MessageBox");
        }
    }
}
