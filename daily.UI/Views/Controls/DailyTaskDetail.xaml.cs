using daily.UI.ViewsModel.DailyWorkViewModel;
using System;
using System.Windows;
using System.Windows.Controls;

namespace daily.UI.Views.Controls
{
    public partial class DailyTaskDetail : UserControl
    {


        internal DailyWorkViewModel DailyWorkViewModel
        {
            get { return (DailyWorkViewModel)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        internal static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(DailyWorkViewModel), typeof(DailyTaskDetail), new PropertyMetadata(null, SetValue));

        private static void SetValue(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DailyTaskDetail dailyTaskDetail = (DailyTaskDetail)d;
            if (dailyTaskDetail != null)
            {
                dailyTaskDetail.DataContext = dailyTaskDetail.DailyWorkViewModel;
            }
        }

        public DailyTaskDetail()
        {
            InitializeComponent();
        }
    }
}
