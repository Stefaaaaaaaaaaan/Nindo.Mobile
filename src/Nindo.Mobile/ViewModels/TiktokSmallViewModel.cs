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
    public class TiktokSmallViewModel : BaseViewModel
    {
        public ICommand RefreshCommand { get; }

        public TiktokSmallViewModel()
        {
            TikTok = new List<Rank>();
            Task.Run(async () => await LoadRanks());

            RefreshCommand = new AsyncCommand(LoadRanks);
        }


        async Task LoadRanks()
        {
            var client = new RanksClient();

            TikTok = (await client.GetLikesScoreboardAsync(RankLikesPlatform.TikTok, Size.Small)).ToList();
        }

        private List<Rank> _tiktok;

        public List<Rank> TikTok
        {
            get => _tiktok;
            set
            {
                _tiktok = value;
                OnPropertyChanged();
            }
        }
    }
}