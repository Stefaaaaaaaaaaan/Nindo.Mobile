using Nindo.Mobile.Services;
using Nindo.Mobile.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nindo.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            Device.SetFlags(new string[] { "Expander_Experimental" });
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
