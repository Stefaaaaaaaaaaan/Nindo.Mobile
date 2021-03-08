using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nindo.Mobile.Models;
using Nindo.Mobile.Services;
using Nindo.Net.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms.Internals;

namespace Nindo.Mobile.ViewModels
{
    public class MilestoneViewModel : ViewModelBase
    {
        #region command
        public IAsyncCommand RefreshCommand { get; }

        #endregion

        private readonly IApiService _apiService;

        public MilestoneViewModel(IApiService apiService)
        {
            Milestones = new List<ExtendedMilestone>
            {
                new ExtendedMilestone
                {
                    MilestoneTitle = "Nächste Meilensteine"
                },
                new ExtendedMilestone
                {
                    MilestoneTitle = "Letzte Meilensteine"
                }
            };

            _apiService = apiService;
            RefreshCommand = new AsyncCommand(RefreshAsync, CanExecute);

        }

        public async Task LoadMilestonesAsync()
        {
            try
            {
                IsBusy = true;

                await Task.Run(async () =>
                {
                    var newMsTask = _apiService.GetMilestonesAsync();
                    var pastMsTask = _apiService.GetPastMilestonesAsync();

                    var taskList = new List<Task<Milestone[]>>
                    {
                        newMsTask,
                        pastMsTask,
                    };

                    Task<Milestone[]> currentTask;

                    while (taskList.Count > 0 && (currentTask = await Task.WhenAny(taskList)) != null)
                    {
                        taskList.Remove(currentTask);

                        if (currentTask == newMsTask)
                        {
                            Milestones[0].Milestones = newMsTask.Result.ToList();
                        }
                        else if (currentTask == pastMsTask)
                        {
                            Milestones[1].Milestones = pastMsTask.Result.ToList();
                        }
                    }
                });
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshAsync()
        {
            try
            {
                IsRefreshing = true;

                Milestones.ForEach(m => m.Milestones.Clear());
                await LoadMilestonesAsync();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private bool CanExecute(object arg)
        {
            return !IsBusy && !IsRefreshing;
        }

        private IList<ExtendedMilestone> _milestones;

        public IList<ExtendedMilestone> Milestones
        {
            get => _milestones;
            set
            {
                _milestones = value;
                OnPropertyChanged();
            }
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
    }
}