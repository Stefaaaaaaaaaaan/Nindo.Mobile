using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Nindo.Mobile.Models;
using Nindo.Mobile.Services;
using Nindo.Net.Models;
using Xamarin.CommunityToolkit.ObjectModel;

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
            Milestones = new ObservableCollection<ExtendedMilestone>();

            _apiService = apiService;
            RefreshCommand = new AsyncCommand(RefreshAsync);

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
                            Milestones.Add(new ExtendedMilestone
                            {
                                MilestoneTitle = "Nächste Meilensteine",
                                Milestone = newMsTask.Result.ToList()
                            });
                        }
                        else if (currentTask == pastMsTask)
                        {
                            Milestones.Add(new ExtendedMilestone
                            {
                                MilestoneTitle = "Letzte Meilensteine",
                                Milestone = pastMsTask.Result.ToList()
                            });
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

                Milestones.Clear();
                await LoadMilestonesAsync();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private bool CanExecute(object arg)
        {
            return !IsBusy;
        }

        private ObservableCollection<ExtendedMilestone> _milestones;

        public ObservableCollection<ExtendedMilestone> Milestones
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