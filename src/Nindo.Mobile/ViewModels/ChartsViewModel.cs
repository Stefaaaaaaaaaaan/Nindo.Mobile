using System.Runtime.CompilerServices;

namespace Nindo.Mobile.ViewModels
{
    public class ChartsViewModel : BaseViewModel
    {
        public ChartsViewModel()
        {
            Title = "Nindo";
            YoutubeViewModel = new YoutubeBigViewModel();
            InstagramViewModel = new InstagraBigViewModel();
            TiktokViewModel = new TiktokBigViewModel();
            TwitterViewModel = new TwitterBigViewModel();
            TwitchViewModel = new TwitchBigViewModel();
        }

        private int _selectedViewModelIndex = 0;

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set => SetAndRaise(ref _selectedViewModelIndex, value);
        }

        private YoutubeBigViewModel _youtubeViewModel;
        public YoutubeBigViewModel YoutubeViewModel
        {
            get => _youtubeViewModel;
            set => SetAndRaise(ref _youtubeViewModel, value);
        }

        private InstagraBigViewModel _instagramViewModel;
        public InstagraBigViewModel InstagramViewModel
        {
            get => _instagramViewModel;
            set => SetAndRaise(ref _instagramViewModel, value);
        }

        private TiktokBigViewModel _tiktokViewModel;
        public TiktokBigViewModel TiktokViewModel
        {
            get => _tiktokViewModel;
            set => SetAndRaise(ref _tiktokViewModel, value);
        }

        private TwitterBigViewModel _twitterViewModel;
        public TwitterBigViewModel TwitterViewModel
        {
            get => _twitterViewModel;
            set => SetAndRaise(ref _twitterViewModel, value);
        }

        private TwitchBigViewModel _twitchViewModel;
        public TwitchBigViewModel TwitchViewModel
        {
            get => _twitchViewModel;
            set => SetAndRaise(ref _twitchViewModel, value);
        }

        protected bool SetAndRaise<T>(ref T property, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(property, value))
            {
                return false;
            }

            property = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}