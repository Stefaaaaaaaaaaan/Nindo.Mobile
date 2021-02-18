using System.Collections.ObjectModel;
using Nindo.Mobile.Models;
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
            var viralDetailPage = new ViralDetailPage();
            var viralDetailPageViewModel = new ViralDetailPageViewModel(viralEntry);

            viralDetailPage.BindingContext = viralDetailPageViewModel;
            await Application.Current.MainPage.Navigation.PushAsync(viralDetailPage, true);
        }
    }
}