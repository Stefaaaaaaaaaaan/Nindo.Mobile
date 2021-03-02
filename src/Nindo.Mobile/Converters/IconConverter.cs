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
                "twitch" => "twitchGrey.png",
                "instagram" => "instagramGrey.png",
                "twitter" => "twitterGrey.png",
                "tiktok" => "tiktokGrey.png",
                "youtube" => "youtubeGrey.png",
                _ => "youtubeGrey.png"
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}