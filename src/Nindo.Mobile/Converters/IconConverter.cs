using System;
using System.Globalization;
using Xamarin.Forms;

namespace Nindo.Mobile.Converters
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var platform = (string) value;

            return platform switch
            {
                "twitch" => "twitch_grey.png",
                "instagram" => "instagram_grey.png",
                "twitter" => "twitter_grey.png",
                "tiktok" => "tiktok_grey.png",
                "youtube" => "youtube_grey.png",
                _ => "youtube_grey.png"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}