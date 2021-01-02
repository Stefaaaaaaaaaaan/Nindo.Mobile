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
    public class InstagramSmallViewModel : BaseViewModel
    {
        public ICommand RefreshCommand { get; }

        public InstagramSmallViewModel()
        {
            Instagram = new List<Rank>();
            Task.Run(async () => await LoadRanks());

            RefreshCommand = new AsyncCommand(LoadRanks);
        }


        async Task LoadRanks()
        {
            var client = new RanksClient();

            Instagram = (await client.GetLikesScoreboardAsync(RankLikesPlatform.Instagram, Size.Small)).ToList();
        }


        private List<Rank> _instagram;

        public List<Rank> Instagram
        {
            get => _instagram;
            set
            {
                _instagram = value;
                OnPropertyChanged();
            }
        }
    }
}
