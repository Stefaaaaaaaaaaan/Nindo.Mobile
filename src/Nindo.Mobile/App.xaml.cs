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
            InitializeComponent();
            MainPage = new AppShell();

            Sharpnado.Tabs.Initializer.Initialize(loggerEnable: false, debugLogEnable: false);
            Sharpnado.Shades.Initializer.Initialize(loggerEnable: false);
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
