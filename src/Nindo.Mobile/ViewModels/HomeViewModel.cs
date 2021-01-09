using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Nindo.Net;
using Nindo.Net.Clients;
using Nindo.Net.Models;
using Nindo.Net.Models.Enums;
using Humanizer;
using MvvmHelpers.Commands;
using Command = Xamarin.Forms.Command;
using Size = Nindo.Net.Models.Enums.Size;


namespace Nindo.Mobile.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Title = "Nindo";
            YoutubeViewModel = new YoutubeSmallViewModel();
            InstagramViewModel = new InstagramSmallViewModel();
            TiktokViewModel = new TiktokSmallViewModel();
            TwitterViewModel = new TwitterSmallViewModel();
            TwitchViewModel = new TwitchSmallViewModel();
        }

        private int _selectedViewModelIndex = 0;

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set => SetAndRaise(ref _selectedViewModelIndex, value);
        }

        private YoutubeSmallViewModel _youtubeViewModel;
        public YoutubeSmallViewModel YoutubeViewModel
        {
            get => _youtubeViewModel;
            set => SetAndRaise(ref _youtubeViewModel, value);
        }

        private InstagramSmallViewModel _instagramViewModel;
        public InstagramSmallViewModel InstagramViewModel
        {
            get => _instagramViewModel;
            set => SetAndRaise(ref _instagramViewModel, value);
        }

        private TiktokSmallViewModel _tiktokViewModel;
        public TiktokSmallViewModel TiktokViewModel
        {
            get => _tiktokViewModel;
            set => SetAndRaise(ref _tiktokViewModel, value);
        }

        private TwitterSmallViewModel _twitterViewModel;
        public TwitterSmallViewModel TwitterViewModel
        {
            get => _twitterViewModel;
            set => SetAndRaise(ref _twitterViewModel, value);
        }

        private TwitchSmallViewModel _twitchViewModel;
        public TwitchSmallViewModel TwitchViewModel
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