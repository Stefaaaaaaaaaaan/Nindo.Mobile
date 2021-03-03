using System.Diagnostics;
using Nindo.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nindo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViralPage : ContentPage
    {
        public ViralPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ViralViewModel vm && vm.ViralData.Count == 0)
                vm.GetViralAsync()
                    .ContinueWith(t =>
                    {
                        if (t.IsFaulted) Debug.WriteLine(t.Exception?.Message);
                    });
        }
    }
}