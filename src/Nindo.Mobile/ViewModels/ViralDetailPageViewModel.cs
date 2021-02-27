using System;
using System.Collections.ObjectModel;
using Nindo.Mobile.Models;
using Nindo.Net.Models;

namespace Nindo.Mobile.ViewModels
{
    public class ViralDetailPageViewModel : ViewModelBase
    {
        public ViralDetailPageViewModel(Viral viral)
        {
            ViralEntry = viral;
            GetContentUrl();
        }

        private void GetContentUrl()
        {
            switch (ViralEntry.Platform)
            {
                case "twitter":
                    ContentUrl = $"https://platform.twitter.com/embed/Tweet.html?dnt=false&embedId=twitter-widget-1&frame=false&hideCard=false&hideThread=false&id={ViralEntry.PostId}&lang=de&origin=https%3A%2F%2Fnindo.de%2Fviral&theme=light&widgetsVersion=889aa01%3A1612811843556&width=100px";
                    Height = 500;
                    Width = 500;
                    break;
                case "tiktok":
                    ContentUrl = $"https://www.tiktok.com/embed/v2/{ViralEntry.PostId}?lang=de";
                    Height = 800;
                    Width = 500;
                    break;
                case "youtube":
                    ContentUrl = $"https://www.youtube.com/embed/{ViralEntry.PostId}";
                    Height = 250;
                    Width = 500;
                    break;
                case "instagram":
                    ContentUrl = $"https://www.instagram.com/p/{ViralEntry.PostId}/embed/?cr=1&v=12&wp=64&rd=https%3A%2F%2Fnindo.de&rp=%2Fviral#%7B%22ci%22%3A0%2C%22os%22%3A1799.8050000001058%2C%22ls%22%3A545.0650000002497%2C%22le%22%3A545.91500000015%7D";
                    Height = 750;
                    Width = 500;
                    break;
            }
        }

        private string _contentUrl;

        public string ContentUrl
        {
            get => _contentUrl;
            set
            {
                _contentUrl = value;
                OnPropertyChanged();
            }
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

        private int _height;

        public int Height
        {
            get => _height;
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        private int _width;

        public int Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }
    }
}