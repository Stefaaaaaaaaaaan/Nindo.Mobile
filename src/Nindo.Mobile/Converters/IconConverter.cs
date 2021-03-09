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
                "twitch" => "twitch_white.png",
                "instagram" => "instagram_white.png",
                "twitter" => "twitter_white.png",
                "tiktok" => "tiktok_white.png",
                "youtube" => "youtube_white.png",
                _ => "youtube_white.png"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}