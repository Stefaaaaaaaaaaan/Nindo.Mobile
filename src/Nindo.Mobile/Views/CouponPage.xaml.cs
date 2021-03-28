using System;
using System.Collections.Generic;
using Nindo.Mobile.Services.Implementations;
using Nindo.Mobile.ViewModels;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
    }
}