using System.Collections.Generic;
using System.Threading.Tasks;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;
using Nindo.Common.Common;
using Nindo.Mobile.Services;
using Nindo.Mobile.Services.Implementations;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;
using Size = Nindo.Net.Models.Enums.Size;

namespace Nindo.Mobile.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        #region commands
        public IAsyncCommand RefreshCommand { get; }
        public Command<string> ChangePlatformCommand { get; }
        #endregion

        public HomeViewModel()
        {
            Title = "Nindo";

            Items = new RangeObservableCollection<Rank>();
            Youtube = new RangeObservableCollection<Rank>();
            Instagram = new RangeObservableCollection<Rank>();
            Tiktok = new RangeObservableCollection<Rank>();
            Twitter = new RangeObservableCollection<Rank>();
            Twitch = new RangeObservableCollection<Rank>();

            RefreshCommand = new AsyncCommand(RefreshRanksAsync, CanExecuteLoad);
            ChangePlatformCommand = new Command<string>(ChangePlatform, CanExecuteLoad);

        }

        public async Task LoadRanksAsync()
        {
            try
            {
                IsBusy = true;

                var apiService = new ApiService();

                await Task.Run(async () =>
                {
                    var youtubeTask = apiService.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Small);
                    var instTask = apiService.GetLikesScoreboardAsync(RankLikesPlatform.Instagram, Size.Small);
                    var ttTask = apiService.GetLikesScoreboardAsync(RankLikesPlatform.TikTok, Size.Small);
                    var twitterTask = apiService.GetLikesScoreboardAsync(RankLikesPlatform.Twitter, Size.Small);
                    var twitchTask = apiService.GetViewersScoreboardAsync(Size.Small);

                    var taskList = new List<Task<Rank[]>>
                    {
                        youtubeTask,
                        instTask,
                        ttTask,
                        twitterTask,
                        twitchTask
                    };

                    Task<Rank[]> currentTask = null;

                    while (taskList.Count > 0 && (currentTask = await Task.WhenAny(taskList)) != null)
                    {
                        taskList.Remove(currentTask);

                        if (currentTask == youtubeTask)
                        {
                            Youtube.AddRange(currentTask.Result);
                            Items.AddRange(Youtube);
                            if (string.IsNullOrEmpty(CurrentPlatform))
                                CurrentPlatform = "youtube";
                        }
                        else if(currentTask == instTask)
                        {
                            Instagram.AddRange(currentTask.Result);
                        }
                        else if (currentTask == ttTask)
                        {
                            Tiktok.AddRange(currentTask.Result);
                        }
                        else if (currentTask == twitterTask)
                        {
                            Twitter.AddRange(currentTask.Result);
                        }
                        else if (currentTask == twitchTask)
                        {
                            Twitch.AddRange(currentTask.Result);
                        }
                    }

                });

            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshRanksAsync()
        {
            ClearCollections();
            CurrentPlatform = "youtube";
            await LoadRanksAsync();
        }

        private void ChangePlatform(string platform)
        {
            try
            {
                IsBusy = true;
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