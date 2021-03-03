using System;
using System.Linq;
using System.Threading.Tasks;
using Nindo.Common.Common;
using Nindo.Mobile.Models;
using Nindo.Mobile.Services;
using Nindo.Mobile.Services.Implementations;
using Nindo.Net.Models;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Nindo.Mobile.ViewModels
{
    public class ViralViewModel : ViewModelBase
    {
        public IAsyncCommand<ViralTypes> OpenDetailPageCommand { get; }

        public ViralViewModel()
        {
            OpenDetailPageCommand = new AsyncCommand<ViralTypes>(OpenDetailPageAsync, CanExecute);
        }

        public async Task GetViralAsync()
        {
            try
            {
                IsBusy = true;

                var apiService = new ApiService();
                var items = await apiService.GetViralsAsync();
                ViralData.AddRange(items);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task OpenDetailPageAsync(ViralTypes type)
        {
            try
            {
                IsBusy = true;

                if (ViralData.Count == 0)
                    await GetViralAsync();

                var viralEntry = type switch
                {
                    ViralTypes.TwitchMaxViewer => ViralData.First(x =>
                        x.Platform == "twitch" && x.Type == "max. zuschauer"),
                    ViralTypes.TwitchLongestStream => ViralData.First(x =>
                        x.Platform == "twitch" && x.Type == "längster stream"),
                    ViralTypes.TwitterLikes => ViralData.First(x =>
                        x.Platform == "twitter" && x.Type == "likes"),
                    ViralTypes.TwitterRetweets => ViralData.First(x =>
                        x.Platform == "twitter" && x.Type == "retweets"),
                    ViralTypes.TiktokLikes => ViralData.First(x =>
                        x.Platform == "tiktok" && x.Type == "likes"),
                    ViralTypes.TiktokComments => ViralData.First(x =>
                        x.Platform == "tiktok" && x.Type == "kommentare"),
                    ViralTypes.TiktokViews => ViralData.First(x =>
                        x.Platform == "tiktok" && x.Type == "views"),
                    ViralTypes.YoutubeLikes => ViralData.First(x =>
                        x.Platform == "youtube" && x.Type == "likes"),
                    ViralTypes.YoutubeComments => ViralData.First(x =>
                        x.Platform == "youtube" && x.Type == "kommentare"),
                    ViralTypes.YoutubeViews => ViralData.First(x =>
                        x.Platform == "youtube" && x.Type == "views"),
                    ViralTypes.InstagramLikes => ViralData.First(x =>
                        x.Platform == "instagram" && x.Type == "likes"),
                    ViralTypes.InstagramComments => ViralData.First(x =>
                        x.Platform == "instagram" && x.Type == "kommentare"),
                    _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
                };

                var nav = new NavigationService();
                await nav.OpenViralDetailPage(viralEntry);
            }
            finally
            {
                IsBusy = false;
            }

        }

        private bool CanExecute(object arg)
        {
            return !IsBusy;
        }

        public RangeObservableCollection<Viral> ViralData { get; } = new RangeObservableCollection<Viral>();
    }
}