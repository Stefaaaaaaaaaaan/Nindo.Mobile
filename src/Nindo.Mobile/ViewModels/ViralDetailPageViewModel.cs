using System.Collections.ObjectModel;
using Nindo.Mobile.Models;
using Nindo.Net.Models;

namespace Nindo.Mobile.ViewModels
{
    public class ViralDetailPageViewModel : BaseViewModel
    {
        public ViralDetailPageViewModel(Viral viral)
        {
            ViralEntry = viral;
        }

        private Viral _viralEntry;

        public Viral ViralEntry
        {
            get => _viralEntry;
            set
            {
                _viralEntry = value;
                OnPropertyChanged();
            }
        }
    }
}