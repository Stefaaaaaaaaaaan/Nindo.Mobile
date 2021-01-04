using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nindo.Net.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nindo.Mobile.Templates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScoreboardSmallTemplate
    {
        public ScoreboardSmallTemplate()
        {
            Ranks = new List<Rank>();
            InitializeComponent();
        }

        public static readonly BindableProperty RanksProperty = BindableProperty.Create(nameof(Ranks), typeof(List<Rank>), typeof(ScoreboardSmallTemplate), string.Empty);


        public List<Rank> Ranks
        {
            get => (List<Rank>)GetValue(RanksProperty);
            set => SetValue(RanksProperty, value);
        }

    }
}