using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Nindo.Net.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nindo.Mobile.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreboardSmallTemplate : ContentView
    {
        public static readonly BindableProperty CollectionViewScoreboardProperty =
            BindableProperty.Create(nameof(CollectionViewScoreboard), typeof(List<Rank>), typeof(ScoreboardSmallTemplate));

        public static readonly BindableProperty BoxViewLeftProperty =
            BindableProperty.Create(nameof(BoxViewLeftColor), typeof(Color), typeof(ScoreboardSmallTemplate));

        public static readonly BindableProperty BoxViewRightProperty =
            BindableProperty.Create(nameof(BoxViewRightColor), typeof(Color), typeof(ScoreboardSmallTemplate));

        public static readonly BindableProperty ImageSourceProperty =
            BindableProperty.Create(nameof(ImageSource), typeof(ImageSource), typeof(ScoreboardSmallTemplate));

        public static readonly BindableProperty RefreshViewCommandProperty =
            BindableProperty.Create(nameof(RefreshViewCommand), typeof(ICommand), typeof(ScoreboardSmallTemplate));

        public static readonly BindableProperty LabelTextProperty =
            BindableProperty.Create(nameof(LabelText), typeof(string), typeof(ScoreboardSmallTemplate));

        public List<Rank> CollectionViewScoreboard
        {
            get => (List<Rank>)GetValue(CollectionViewScoreboardProperty);
            set => SetValue(CollectionViewScoreboardProperty, value);
        }

        public Color BoxViewLeftColor
        {
            get => (Color)GetValue(BoxViewLeftProperty);
            set => SetValue(BoxViewLeftProperty, value);
        }

        public Color BoxViewRightColor
        {
            get => (Color)GetValue(BoxViewRightProperty);
            set => SetValue(BoxViewRightProperty, value);
        }

        public ImageSource ImageSource
        {
            get => (ImageSource)GetValue(ImageSourceProperty);
            set => SetValue(ImageSourceProperty, value);
        }

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


        public ScoreboardSmallTemplate()
        {
            InitializeComponent();

            InnerCollectionView.SetBinding(ItemsView.ItemsSourceProperty, new Binding(nameof(CollectionViewScoreboard), source: this));
            InnerBoxViewLeft.SetBinding(BoxView.ColorProperty, new Binding(nameof(BoxViewLeftColor), source: this));
            InnerBoxViewRight.SetBinding(BoxView.ColorProperty, new Binding(nameof(BoxViewRightColor), source: this));
            InnerImage.SetBinding(Image.SourceProperty, new Binding(nameof(ImageSource), source: this));
            InnerRefreshView.SetBinding(RefreshView.CommandProperty, new Binding(nameof(RefreshViewCommand), source: this));
            InnerLabel.SetBinding(Label.TextProperty, new Binding(nameof(LabelText), source: this));
        }
    }
}