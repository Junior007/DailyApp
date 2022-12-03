﻿using System;
using System.Windows;
using System.Windows.Input;

namespace daily.UI.ViewsModel.MainView
{


    public class AddCommand : ICommand
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
            MessageBoxResult result = MessageBox.Show("AddCommand Hello MessageBox");
        }
    }
}
