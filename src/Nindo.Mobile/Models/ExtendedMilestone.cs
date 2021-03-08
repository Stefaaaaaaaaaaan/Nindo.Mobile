using System.Collections.Generic;
using Nindo.Mobile.ViewModels;
using Nindo.Net.Models;

namespace Nindo.Mobile.Models
{
    public class ExtendedMilestone : ViewModelBase
    {
        public string MilestoneTitle { get; set; }

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