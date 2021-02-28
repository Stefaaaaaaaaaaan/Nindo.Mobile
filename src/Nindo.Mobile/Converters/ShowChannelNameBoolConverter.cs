using System;
using System.Globalization;
using Xamarin.Forms;

namespace Nindo.Mobile.Converters
{
    public class ShowChannelNameBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return false;
            var showChannelName = (bool)value;
            if (showChannelName) 
                return true;
            return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}