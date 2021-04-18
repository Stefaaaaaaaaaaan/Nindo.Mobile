using Nindo.Mobile.Services.Implementations;
using Nindo.Mobile.ViewModels;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Diagnostics;

namespace Nindo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CouponPage : ContentPage
    {
        public CouponPage()
        {
            BindingContext = new CouponViewModel(new ApiService());
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is CouponViewModel vm) {


                if (!vm.Coupons[2].ComboboxItems.Any())
                {
                    vm.LoadCategorys()
                        .ContinueWith(t =>
                        {
                            if (t.IsFaulted) Debug.WriteLine(t.Exception?.Message);
                        });
                }
                if (!vm.Coupons[1].ComboboxItems.Any())
                {
                    vm.LoadBrands()
                        .ContinueWith(t =>
                        {
                            if (t.IsFaulted) Debug.WriteLine(t.Exception?.Message);
                        });
                }
            }
        }
    }
}