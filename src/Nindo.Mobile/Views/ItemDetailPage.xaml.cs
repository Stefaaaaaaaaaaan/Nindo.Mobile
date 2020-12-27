using Nindo.Mobile.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace Nindo.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}