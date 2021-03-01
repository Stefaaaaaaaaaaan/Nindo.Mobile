using System;
using System.Globalization;
using Nindo.Net.Models;
using Xamarin.Forms;

namespace Nindo.Mobile.Converters
{
    public class IconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var platform = (string)value;
            switch (platform)
            {
                case "twitch":
                    return "twitchGrey.png";
                case "instagram":
                    return "instagramGrey.png";
                case "twitter":
                    return "twitterGrey.png";
                case "tiktok":
                    return "tiktokGrey.png";
                case "youtube":
                    return "youtubeGrey.png";
                default:
                    return "youtubeGrey.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
