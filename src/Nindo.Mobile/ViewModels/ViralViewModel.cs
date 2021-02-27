using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Nindo.Common.Common;
using Nindo.Mobile.Models;
using Nindo.Mobile.Services;
using Nindo.Net;
using Nindo.Net.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace Nindo.Mobile.ViewModels
{
    public class ViralViewModel : ViewModelBase
    {
        public Command<ViralTypes> OpenDetailPageAsyncCommand { get; }

        public ViralViewModel()
        {
            Task.Run(async () => await GetViralAsync());
            var test = ViralData;
            var ree = test;

            OpenDetailPageAsyncCommand = new Command<ViralTypes>(OpenDetailPageAsync);
            ViralData = new ObservableCollection<Viral>();
        }

        private async Task GetViralAsync()
        {
            var client = new NindoClient();
            var items = await client.GetViralsAsync();
            foreach (var i in items)
            {
                ViralData.Add(i);
            }
        }

        private void OpenDetailPageAsync(ViralTypes type)
        {
            var viralEntry = type switch
            {
                ViralTypes.TwitchMaxViewer => ViralData.First(x => x.Platform == "twitch" && x.Type == "max. zuschauer"),
                ViralTypes.TwitchLongestStream => ViralData.First(x => x.Platform == "twitch" && x.Type == "längster stream"),
                ViralTypes.TwitterLikes => ViralData.First(x => x.Platform == "twitter" && x.Type == "likes"),
                ViralTypes.TwitterRetweets => ViralData.First(x => x.Platform == "twitter" && x.Type == "retweets"),
                ViralTypes.TiktokLikes => ViralData.First(x => x.Platform == "tiktok" && x.Type == "likes"),
                ViralTypes.TiktokComments => ViralData.First(x => x.Platform == "tiktok" && x.Type == "kommentare"),
                ViralTypes.TiktokViews => ViralData.First(x => x.Platform == "tiktok" && x.Type == "views"),
                ViralTypes.YoutubeLikes => ViralData.First(x => x.Platform == "youtube" && x.Type == "likes"),
                ViralTypes.YoutubeComments => ViralData.First(x => x.Platform == "youtube" && x.Type == "kommentare"),
                ViralTypes.YoutubeViews => ViralData.First(x => x.Platform == "youtube" && x.Type == "views"),
                ViralTypes.InstagramLikes => ViralData.First(x => x.Platform == "instagram" && x.Type == "likes"),
                ViralTypes.InstagramComments => ViralData.First(x => x.Platform == "instagram" && x.Type == "kommentare"),
                _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
            };

            var nav = new NavigationService();
            nav.OpenViralDetailPage(viralEntry);
        }

        private bool CanExecuteLoad(object arg)
        {
            return !IsBusy;
        }

        private ObservableCollection<Viral> _viralDataData;

        public ObservableCollection<Viral> ViralData
        {
            get => _viralDataData;
            set
            {
                _viralDataData = value;
                OnPropertyChanged();
            }
        }
    }
}