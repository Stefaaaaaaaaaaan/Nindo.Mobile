using Nindo.Mobile.ViewModels;
using Nindo.Mobile.Views;
using Nindo.Net.Models;
using Xamarin.Forms;

namespace Nindo.Mobile.Services
{
    public class NavigationService
    {
        public async void OpenViralDetailPage(Viral viralEntry)
        {
            if (viralEntry.Platform.Equals("twitch"))
            {
                var twitchViralDetailPage = new TwitchViralDetailPage(viralEntry);
                await Application.Current.MainPage.Navigation.PushAsync(twitchViralDetailPage, true);
            }
            else
            {
                var viralDetailPage = new ViralDetailPage(viralEntry);
                await Application.Current.MainPage.Navigation.PushAsync(viralDetailPage, true);
            }
        }
    }
}