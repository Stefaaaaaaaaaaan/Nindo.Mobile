using System;
using System.Globalization;
using Xamarin.Forms;

namespace Nindo.Mobile.Converters
{
    public class UlongToDecimal : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var number = (ulong)value;
            return System.Convert.ToDouble(number).ToString("N0", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
