using System;
using System.Globalization;
using Nindo.Net.Models;
using Xamarin.Forms;

namespace Nindo.Mobile.Services
{
    public class ShowChannelNameBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            var rank = (Rank)value;
            if (rank.ShowChannelName) 
                return true;
            return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}