using System.Threading.Tasks;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;
using Nindo.Common.Common;
using Nindo.Net;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Size = Nindo.Net.Models.Enums.Size;

namespace Nindo.Mobile.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        public IAsyncCommand LoadCommand { get; }
        public Command<string> ChangePlatformCommand { get; }

        public HomeViewModel()
        {
            Title = "Nindo";
            Items = new RangeObservableCollection<Rank>();
            Youtube = new RangeObservableCollection<Rank>();
            Instagram = new RangeObservableCollection<Rank>();
            Tiktok = new RangeObservableCollection<Rank>();
            Twitter = new RangeObservableCollection<Rank>();
            Twitch = new RangeObservableCollection<Rank>();

            Task.Run(async () => await LoadRanksAsync());

            LoadCommand = new AsyncCommand(LoadRanksAsync, CanExecuteLoad);
            ChangePlatformCommand = new Command<string>(ChangePlatform, CanExecuteLoad);

        }

        private async Task LoadRanksAsync()
        {
            try
            {
                if (Items.Count > 0)
                {
                    ClearCollections();
                    CurrentPlatform = "youtube";
                }

                var client = new NindoClient();
                await Task.Run(async () =>
                {
                    Youtube.AddRange(await client.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Small));
                    Items.AddRange(Youtube);
                    if(string.IsNullOrEmpty(CurrentPlatform))
                        CurrentPlatform = "youtube";
                });
                await Task.Run(async () =>
                {
                    Instagram.AddRange(await client.GetLikesScoreboardAsync(RankLikesPlatform.Instagram, Size.Small));
                    Tiktok.AddRange(await client.GetLikesScoreboardAsync(RankLikesPlatform.TikTok, Size.Small));
                    Twitter.AddRange(await client.GetLikesScoreboardAsync(RankLikesPlatform.Twitter, Size.Small));
                    Twitch.AddRange(await client.GetViewersScoreboardAsync(Size.Small));
                });

            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ChangePlatform(string platform)
        {
            try
            {
                if (CurrentPlatform == platform)
                    return;

                switch (platform)
                {
                    case "youtube":
                        Items.Clear();
                        Items.AddRange(Youtube);
                        CurrentPlatform = "youtube";
                        break;
                    case "instagram":
                        Items.Clear();
                        Items.AddRange(Instagram);
                        CurrentPlatform = "instagram";
                        break;
                    case "tiktok":
                        Items.Clear();
                        Items.AddRange(Tiktok);
                        CurrentPlatform = "tiktok";
                        break;
                    case "twitter":
                        Items.Clear();
                        Items.AddRange(Twitter);
                        CurrentPlatform = "twitter";
                        break;
                    case "twitch":
                        Items.Clear();
                        Items.AddRange(Twitch);
                        CurrentPlatform = "twitch";
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

        private void ClearCollections()
        {
            Items.Clear();
            Youtube.Clear();
            Instagram.Clear();
            Tiktok.Clear();
            Twitter.Clear();
            Twitch.Clear();
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

        private RangeObservableCollection<Rank> _youtube;

        public RangeObservableCollection<Rank> Youtube
        {
            get => _youtube;
            set
            {
                _youtube = value;
                OnPropertyChanged();
            }
        }


        private RangeObservableCollection<Rank> _instagram;

        public RangeObservableCollection<Rank> Instagram
        {
            get => _instagram;
            set
            {
                _instagram = value;
                OnPropertyChanged();
            }
        }

        private RangeObservableCollection<Rank> _tiktok;

        public RangeObservableCollection<Rank> Tiktok
        {
            get => _tiktok;
            set
            {
                _tiktok = value;
                OnPropertyChanged();
            }
        }

        private RangeObservableCollection<Rank> _twitter;

        public RangeObservableCollection<Rank> Twitter
        {
            get => _twitter;
            set
            {
                _twitter = value;
                OnPropertyChanged();
            }
        }

        private RangeObservableCollection<Rank> _twitch;

        public RangeObservableCollection<Rank> Twitch
        {
            get => _twitch;
            set
            {
                _twitch = value;
                OnPropertyChanged();
            }
        }

        private string _currentPlatform;

        public string CurrentPlatform
        {
            get => _currentPlatform;
            set
            {
                if (_currentPlatform == value) return;
                _currentPlatform = value;
                OnPropertyChanged();
            }
        }
    }
}