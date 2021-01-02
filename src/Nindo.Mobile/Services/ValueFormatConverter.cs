using System;
using System.Globalization;
using Humanizer;
using Nindo.Net.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nindo.Mobile.Services
{
    public class ValueFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var rank = (Rank) value;
            return System.Convert.ToDouble(rank.Value).ToMetric(decimals:1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            return System.Convert.ToString(value).FromMetric();
        }
    }
}