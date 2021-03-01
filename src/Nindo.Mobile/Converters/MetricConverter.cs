using System;
using System.Globalization;
using Humanizer;
using Xamarin.Forms;

namespace Nindo.Mobile.Converters
{
    public class MetricConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var number = (long)value;
            return System.Convert.ToDouble(number).ToMetric(decimals:1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var metric = (string)value;
            return System.Convert.ToString(metric).FromMetric();
        }
    }
}