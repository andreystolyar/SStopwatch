using System;
using System.Globalization;
using System.Windows.Data;

namespace SStopwatch
{
    internal class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(targetType != typeof(string))
                throw new ArgumentException("targetType");

            var timeSpan = (TimeSpan)value;

            //return $"{(int)timeSpan.TotalHours}";
            return $"{(int)timeSpan.TotalHours}:{timeSpan.Minutes}:{timeSpan.Seconds}";
            //return timeSpan.ToString(@"hh\:mm\:ss");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
