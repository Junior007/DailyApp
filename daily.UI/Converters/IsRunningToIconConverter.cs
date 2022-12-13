using System;
using System.Globalization;
using System.Windows.Data;

namespace daily.UI.Converters
{
    internal class IsRunningToIconConverter : IValueConverter
    {

        string play = "\uE102";
        string stop = "\uE769";
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isRunning = (bool)value;
            return isRunning ? stop : play;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string state = value as string;
            return state==play;
        }
    }
}
