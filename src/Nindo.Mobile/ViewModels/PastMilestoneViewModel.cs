using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmHelpers.Commands;
using Nindo.Mobile.Services.Implementations;
using Nindo.Net.Models;

namespace Nindo.Mobile.ViewModels
{
    class PastMilestoneViewModel : ViewModelBase
    {
        public ICommand RefreshCommand { get; }

        public PastMilestoneViewModel()
        {

            Milestones = new List<Milestone>();

            Task.Run(async () => await LoadRanks());

            RefreshCommand = new AsyncCommand(LoadRanks);
        }

        async Task LoadRanks()
        {
            var apiService = new ApiService();

            Milestones = (await apiService.GetPastMilestonesAsync()).ToList();
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
