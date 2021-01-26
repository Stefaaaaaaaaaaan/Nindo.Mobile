using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Nindo.Net.Clients;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;

namespace Nindo.Mobile.ViewModels
{
    public class YoutubeBigViewModel : BaseViewModel
    {
        public ICommand LoadItemsCommand { get; }

        public ObservableRangeCollection<Rank> Items { get; set; }
        public ObservableRangeCollection<Rank> AllItems { get; set; }
        public ObservableRangeCollection<string> FilterOptions { get; }

        private string _selectedFilter;
        public string SelectedFilter
        {
            get => _selectedFilter;
            set
            {
                if (SetProperty(ref _selectedFilter, value))
                    FilterItems();
            }
        }

        public YoutubeBigViewModel()
        {
            Items = new ObservableRangeCollection<Rank>();
            AllItems = new ObservableRangeCollection<Rank>();
            LoadItemsCommand = new Command(async () => await LoadRanks());

            FilterOptions = new ObservableRangeCollection<string>
            {
                "Views",
                "Likes",
                "Neue Abos",
                "Abos",
                "YouTube-Rang"
            };
            SelectedFilter = "Views";
        }

        async Task LoadRanks()
        {
            var client = new RanksClient();

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = (await client.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Big)).ToList();
                AllItems.ReplaceRange(items);
                FilterItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void FilterItems()
        {
            var client = new RanksClient();

            switch (SelectedFilter)
            {
                case "Views":
                    Items.ReplaceRange(await Task.Run(() => client.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Big)));
                    break;
                case "Likes":
                    Items.ReplaceRange(await Task.Run(() => client.GetLikesScoreboardAsync(RankLikesPlatform.Youtube, Size.Big)));
                    break;
                case "Neue Abos":
                    Items.ReplaceRange(await Task.Run(() => client.GetSubGainScoreboardAsync(RankAllPlatform.Youtube, Size.Big)));
                    break;
                case "Abos":
                    Items.ReplaceRange(await Task.Run(() => client.GetScoreboardAsync(RankAllPlatform.Youtube, Size.Big)));
                    break;
                case "YouTube-Rang":
                    Items.ReplaceRange(await Task.Run(() => client.GetScoreboardAsync(RankAllPlatform.Youtube, Size.Big)));
                    break;
            }

        }

    }

}