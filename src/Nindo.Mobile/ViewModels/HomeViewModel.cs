using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Nindo.Net;
using Nindo.Net.Clients;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;
using Humanizer;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using Size = Nindo.Net.Models.Enums.Size;


namespace Nindo.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public IAsyncCommand<string> LoadCommand { get; }

        public HomeViewModel()
        {
            Title = "Nindo";
            Items = new List<Rank>();
            Task.Run(async () => await LoadRanksAsync("youtube"));

            LoadCommand = new AsyncCommand<string>(LoadRanksAsync, CanExecuteLoad);
        }

        private async Task LoadRanksAsync(string platform)
        {
            try
            {
                var client = new RanksClient();
                switch (platform)
                {
                    case "youtube":
                        Items = await Task.Run(async () => (await client.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Small)).ToList());
                        CurrentPlatform = "youtube";
                        break;
                    case "instagram":
                        Items = await Task.Run(async () => (await client.GetLikesScoreboardAsync(RankLikesPlatform.Instagram, Size.Small)).ToList());
                        CurrentPlatform = "instagram";
                        break;
                    case "tiktok":
                        Items = await Task.Run(async () => (await client.GetLikesScoreboardAsync(RankLikesPlatform.TikTok, Size.Small)).ToList());
                        CurrentPlatform = "tiktok";
                        break;
                    case "twitter":
                        Items = await Task.Run(async () => (await client.GetLikesScoreboardAsync(RankLikesPlatform.Twitter, Size.Small)).ToList());
                        CurrentPlatform = "twitter";
                        break;
                    case "twitch":
                        Items = await Task.Run(async () => (await client.GetViewersScoreboardAsync(RankViewerPlatform.Twitch, Size.Small)).ToList());
                        CurrentPlatform = "twitch";
                        break;
                    default:
                        Items = await Task.Run(async () => (await client.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Small)).ToList());
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

        private List<Rank> _items;

        public List<Rank> Items
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