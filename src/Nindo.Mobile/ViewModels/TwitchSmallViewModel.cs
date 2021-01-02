using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Nindo.Net.Clients;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;

namespace Nindo.Mobile.ViewModels
{
    public class TwitchSmallViewModel : BaseViewModel
    {
        public ICommand RefreshCommand { get; }

        public TwitchSmallViewModel()
        {
            Twitch = new List<Rank>();
            Task.Run(async () => await LoadRanks());

            RefreshCommand = new AsyncCommand(LoadRanks);
        }


        async Task LoadRanks()
        {
            var client = new RanksClient();

            Twitch = (await client.GetViewersScoreboardAsync(RankViewerPlatform.Twitch, Size.Small)).ToList();
        }

        private List<Rank> _twitch;
        public List<Rank> Twitch
        {
            get => _twitch;
            set
            {
                _twitch = value;
                OnPropertyChanged();
            }
        }
    }
}