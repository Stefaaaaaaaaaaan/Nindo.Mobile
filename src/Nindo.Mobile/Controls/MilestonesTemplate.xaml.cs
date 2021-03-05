using System.Collections.Generic;
using System.Windows.Input;
using Nindo.Net.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nindo.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MilestonesTemplate : ContentView
    {
        public static readonly BindableProperty LabelTextProperty =
            BindableProperty.Create(nameof(LabelText), typeof(string), typeof(MilestonesTemplate));

        public static readonly BindableProperty CollectionViewScoreboardProperty =
            BindableProperty.Create(nameof(CollectionViewScoreboard), typeof(List<Milestone>),
                typeof(MilestonesTemplate));


        public MilestonesTemplate()
        {
            InitializeComponent();

            InnerCollectionView.SetBinding(ItemsView.ItemsSourceProperty,
                new Binding(nameof(CollectionViewScoreboard), source: this));
          
            Label.SetBinding(Label.TextProperty, new Binding(nameof(LabelText), source: this));
        }

        public string LabelText
        {
            get => (string) GetValue(LabelTextProperty);
            set => SetValue(LabelTextProperty, value);
        }

        public List<Milestone> CollectionViewScoreboard
        {
            get => (List<Milestone>) GetValue(CollectionViewScoreboardProperty);
            set => SetValue(CollectionViewScoreboardProperty, value);
        }
    }
}