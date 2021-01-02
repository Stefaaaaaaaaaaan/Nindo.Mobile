using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Nindo.Net.Clients;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;

namespace Nindo.Mobile.ViewModels
{
    public class YoutubeSmallViewModel : BaseViewModel
    {

        public ICommand RefreshCommand { get; }

        public YoutubeSmallViewModel()
        {
            Title = "Nindo";
            Youtube = new List<Rank>();
            Task.Run(async () => await LoadRanks());

            RefreshCommand = new AsyncCommand(LoadRanks);
        }


        async Task LoadRanks()
        {
            var client = new RanksClient();

            Youtube = (await client.GetViewsScoreboardAsync(RankViewsPlatform.Youtube, Size.Small)).ToList();
        }

        private List<Rank> _youtube;

        public List<Rank> Youtube
        {
            get => _youtube;
            set
            {
                _youtube = value;
                OnPropertyChanged();
            }
        }
    }
}