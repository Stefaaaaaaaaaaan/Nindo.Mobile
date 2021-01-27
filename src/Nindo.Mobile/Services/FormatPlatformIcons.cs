using Nindo.Net.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Nindo.Mobile.Services
{
    class FormatPlatformIcons : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var milestone = (Milestone)value;
            switch (milestone.MilestoneChannel.Platform)
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
