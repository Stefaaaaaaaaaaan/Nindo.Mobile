using System.Collections.Generic;
using Nindo.Net.Models;

namespace Nindo.Mobile.Models
{
    public class ExtendedMilestone
    {
        public string MilestoneTitle { get; set; }

        public List<Milestone> Milestone { get; set; }
    }
}