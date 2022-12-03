﻿using daily.application.Models;
using daily.application.Service;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace daily.UI.ViewsModel.MainView
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private IDataServices _dataService;
        public int MyProperty { get; set; }

        public ICommand _addCommand;
        public ICommand AddCommand  { get => _addCommand; }
        public ICommand _deleteCommand;
        public ICommand DeleteCommand { get => _deleteCommand; }

        public MainViewModel(IDataServices dataService)
        {
            _dataService = dataService ?? throw new ArgumentNullException(nameof(dataService));

            _addCommand = new AddCommand();
            _deleteCommand = new DeleteCommand();

            DataDetails = new ObservableCollection<DataDetail>(_dataService.GetDataDetails());
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ObservableCollection<DataDetail> DataDetails { get; set; }

        private DataDetail _dataDetail;
        public DataDetail DataDetail
        {
            get => _dataDetail;
            set
            {
                _dataDetail = value;
                OnPropertyChanged();
            }
        }



    }
}
