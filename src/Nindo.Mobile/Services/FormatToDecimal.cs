using Nindo.Net.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Nindo.Mobile.Services
{
    public class FormatToDecimal : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var milestone = (Milestone)value;
            culture = CultureInfo.CreateSpecificCulture("de-DE");
            return System.Convert.ToDouble(milestone.CurrentSubs).ToString("N0", culture);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
