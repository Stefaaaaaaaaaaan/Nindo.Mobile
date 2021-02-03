using System.Threading.Tasks;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Nindo.Common.Common;
using Nindo.Net;
using Size = Nindo.Net.Models.Enums.Size;


namespace Nindo.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IAsyncCommand<string> LoadCommand { get; }

        public HomeViewModel()
        {
            Title = "Nindo";
            Items = new RangeObservableCollection<Rank>();
            Task.Run(async () => await LoadRanksAsync("youtube"));

            LoadCommand = new AsyncCommand<string>(LoadRanksAsync, CanExecuteLoad);
        }

        private async Task LoadRanksAsync(string platform)
        {
            try
            {
                var client = new NindoClient();
                switch (platform)
                {
                    case "youtube":
                        Items.Clear();
                        Items.AddRange(await client.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Small));
                        CurrentPlatform = "youtube";
                        break;
                    case "instagram":
                        Items.Clear();
                        Items.AddRange(await client.GetLikesScoreboardAsync(RankLikesPlatform.Instagram, Size.Small));
                        CurrentPlatform = "instagram";
                        break;
                    case "tiktok":
                        Items.Clear();
                        Items.AddRange(await client.GetLikesScoreboardAsync(RankLikesPlatform.TikTok, Size.Small));
                        CurrentPlatform = "tiktok";
                        break;
                    case "twitter":
                        Items.Clear();
                        Items.AddRange(await client.GetLikesScoreboardAsync(RankLikesPlatform.Twitter, Size.Small));
                        CurrentPlatform = "twitter";
                        break;
                    case "twitch":
                        Items.Clear();
                        Items.AddRange(await client.GetViewersScoreboardAsync(Size.Small));
                        CurrentPlatform = "twitch";
                        break;
                    default:
                        Items.Clear();
                        Items.AddRange(await client.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Small));
                        CurrentPlatform = "youtube";
                        break;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
        private bool CanExecuteLoad(object arg)
        {
            return !IsBusy;
        }

        private RangeObservableCollection<Rank> _items;

        public RangeObservableCollection<Rank> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(); 
            }
        }

        private string _currentPlatform;

        public string CurrentPlatform
        {
            get => _currentPlatform;
            set
            {
                _currentPlatform = value;
                OnPropertyChanged();
            }
        }
    }
}