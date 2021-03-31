using System;
using System.Collections.Generic;
using Nindo.Mobile.Services.Implementations;
using Nindo.Mobile.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            if (BindingContext is CouponViewModel vm && !vm.CategoryItems.Any())
            {
                vm.LoadCategorys()
                    .ContinueWith(t =>
                    {
                        if (t.IsFaulted) Debug.WriteLine(t.Exception?.Message);
                    });
            }
            if (BindingContext is CouponViewModel bvm && !bvm.BrandItems.Any())
            {
                bvm.LoadBrands()
                    .ContinueWith(t =>
                    {
                        if (t.IsFaulted) Debug.WriteLine(t.Exception?.Message);
                    });
            }
        }
    }
}