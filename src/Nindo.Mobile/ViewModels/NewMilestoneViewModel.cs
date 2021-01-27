using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Nindo.Net.Clients;
using Nindo.Net.Models;

namespace Nindo.Mobile.ViewModels
{
    public class NewMilestoneViewModel : BaseViewModel
    {
        public ICommand RefreshCommand { get; }

        public NewMilestoneViewModel()
        {

            Milestones = new List<Milestone>();

            Task.Run(async () => await LoadRanks());

            RefreshCommand = new AsyncCommand(LoadRanks);
        }

        async Task LoadRanks()
        {
            var client = new RanksClient();

            Milestones = (await client.GetMilestonesAsync()).ToList();
        }

        private List<Milestone> _milestones;

        public List<Milestone> Milestones
        {
            get => _milestones;
            set
            {
                _milestones = value;
                OnPropertyChanged();
            }
        }
    }
}