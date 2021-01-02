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
    public class TwitterSmallViewModel : BaseViewModel
    {
        public ICommand RefreshCommand { get; }

        public TwitterSmallViewModel()
        {
            Twitter = new List<Rank>();
            Task.Run(async () => await LoadRanks());

            RefreshCommand = new AsyncCommand(LoadRanks);
        }


        async Task LoadRanks()
        {
            var client = new RanksClient();

            Twitter = (await client.GetLikesScoreboardAsync(RankLikesPlatform.Twitter, Size.Small)).ToList();
        }

        private List<Rank> _twitter;

        public List<Rank> Twitter
        {
            get => _twitter;
            set
            {
                _twitter = value;
                OnPropertyChanged();
            }
        }
    }
}