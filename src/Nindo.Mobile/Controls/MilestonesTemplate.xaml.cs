using Nindo.Net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nindo.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MilestonesTemplate : ContentView
    {
        public static readonly BindableProperty RefreshViewCommandProperty =
            BindableProperty.Create(nameof(RefreshViewCommand), typeof(ICommand), typeof(MilestonesTemplate));

        public static readonly BindableProperty LabelTextProperty =
            BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MilestonesTemplate));

        public static readonly BindableProperty CollectionViewScoreboardProperty =
            BindableProperty.Create(nameof(CollectionViewScoreboard), typeof(List<Milestone>), typeof(MilestonesTemplate));

        public ICommand RefreshViewCommand
        {
            get => (ICommand)GetValue(RefreshViewCommandProperty);
            set => SetValue(RefreshViewCommandProperty, value);
        }

        public string LabelText
        {
            get => (string)GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public List<Milestone> CollectionViewScoreboard
        {
            get => (List<Milestone>)GetValue(CollectionViewScoreboardProperty);
            set => SetValue(CollectionViewScoreboardProperty, value);
        }


        public MilestonesTemplate()
        {
            InitializeComponent();

            InnerCollectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(CollectionViewScoreboard), source: this));
            InnerRefreshView.SetBinding(RefreshView.CommandProperty, new Binding(nameof(RefreshViewCommand), source: this));
            Label.SetBinding(Label.TextProperty, new Binding(nameof(LabelText), source: this));
        }
    }
}