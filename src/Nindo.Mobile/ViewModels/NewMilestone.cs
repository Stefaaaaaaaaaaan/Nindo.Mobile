using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Nindo.Net;
using Nindo.Net.Models;

namespace Nindo.Mobile.ViewModels
{
    public class NewMilestone : ViewModelBase
    {
        public ICommand RefreshCommand { get; }

        public NewMilestone()
        {

            Milestones = new List<Milestone>();

            Task.Run(async () => await LoadRanks());

            RefreshCommand = new AsyncCommand(LoadRanks);
        }

        async Task LoadRanks()
        {
            var client = new NindoClient();

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