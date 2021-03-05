using System.Diagnostics;
using Nindo.Mobile.Services.Implementations;
using Nindo.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nindo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MilestonePage : TabbedPage
    {
        public MilestonePage()
        {
            BindingContext = new MilestoneViewModel(new ApiService());
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is MilestoneViewModel vm && vm.Milestones.Count == 0)
                vm.LoadMilestonesAsync()
                    .ContinueWith(t =>
                    {
                        if (t.IsFaulted) Debug.WriteLine(t.Exception?.Message);
                    });
        }
    }
}