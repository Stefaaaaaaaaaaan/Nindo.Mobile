using Nindo.Net.Models;

namespace Nindo.Mobile.ViewModels
{
    public class TwitchViralDetailPageViewModel : ViewModelBase
    {
        public TwitchViralDetailPageViewModel(Viral viral)
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