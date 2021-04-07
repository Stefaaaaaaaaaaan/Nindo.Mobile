using Xamarin.CommunityToolkit.ObjectModel;
using Nindo.Mobile.Services;
using Nindo.Net.Models;
using System.Threading.Tasks;
using Nindo.Common.Common;
using AsyncCommand = Xamarin.CommunityToolkit.ObjectModel.AsyncCommand;
using Xamarin.Essentials;

namespace Nindo.Mobile.ViewModels
{
    class CouponViewModel : ViewModelBase
    {
        #region command
        public IAsyncCommand RefreshCommand { get; }
        public IAsyncCommand LoadMoreCouponsCommand { get; set; }
        public IAsyncCommand SelectionChangedCommand { get; set; }
        public IAsyncCommand<string> FilterCommand { get; }
        #endregion

        private readonly IApiService _apiService;
        private int _pageNumber;
        private string currentFilter;

        private RangeObservableCollection<string> _categoryItems;
        private RangeObservableCollection<CouponBrands> _brandItems;

        private string _categorySelectedItem;
        private CouponBrands _brandSelectedItem;
        private Coupon _collectionViewSelectedItem;

        public RangeObservableCollection<Coupon> Items { get; set; }

        public CouponViewModel(IApiService apiService)
        {
            Title = "Coupons";
            _apiService = apiService;

            Items = new RangeObservableCollection<Coupon>();
            BrandItems = new RangeObservableCollection<CouponBrands>();
            CategoryItems = new RangeObservableCollection<string>();

            LoadMoreCouponsCommand = new AsyncCommand(SelectItemsSource);
            RefreshCommand = new AsyncCommand(RefreshAsync, CanExecute);
            SelectionChangedCommand = new AsyncCommand(CopyCouponCode);

            FilterCommand = new AsyncCommand<string>(Filter, CanExecute);
        }

        public async Task SelectItemsSource()
        {
            if (CategoryIsVisible == false && BrandIsVisible == false)
            {
                if (IsBusy)
                    return;

                try
                {
                    IsBusy = true;
                    var coupon = await _apiService.GetCouponsAsync(_pageNumber);
                    Items.AddRange(coupon.Coupon);
                    if (coupon.HasMore == "true")
                    {
                        _pageNumber += 20;
                    }
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else if (CategoryIsVisible == true && BrandIsVisible == false)
            {
                try
                {
                    IsBusy = true;
                    if (Items.Count == 0)
                    {

                    }
                    var brand = await _apiService.GetCouponsByBranchAsync(_brandSelectedItem.Id, _pageNumber);
                    Items.AddRange(brand.Coupon);
                    if (brand.HasMore == "true")
                    {
                        _pageNumber += 20;
                    }
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else if (CategoryIsVisible == false && BrandIsVisible == true)
            {
                try
                {
                    IsBusy = true;
                    var category = await _apiService.GetCouponsByCategoryAsync(CategorySelectedItem, _pageNumber);
                    Items.AddRange(category.Coupon);
                    if (category.HasMore == "true")
                    {
                        _pageNumber += 20;
                    }
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        public async Task CopyCouponCode()
        {
            await Clipboard.SetTextAsync(CollectionViewSelectedItem.Code);
            await App.Current.MainPage.DisplayAlert("", "Code has been Copied", "UwU");
        }

        public async Task LoadCategorys()
        {
            var categorys = await _apiService.GetCouponBranchesAsync();
            CategoryItems.AddRange(categorys);
        }

        public async Task LoadBrands()
        {
            var brands = await _apiService.GetCouponBrandsAsync();
            BrandItems.AddRange(brands);
        }

        public RangeObservableCollection<string> CategoryItems
        {
            get => _categoryItems;
            set
            {
                _categoryItems = value;
                OnPropertyChanged();
            }
        }


        public RangeObservableCollection<CouponBrands> BrandItems
        {
            get => _brandItems;
            set
            {
                _brandItems = value;
                OnPropertyChanged();
            }
        }

        public Coupon CollectionViewSelectedItem
        {
            get => _collectionViewSelectedItem;
            set
            {
                _collectionViewSelectedItem = value;

                OnPropertyChanged();
            }
        }

        public string CategorySelectedItem
        {
            get => _categorySelectedItem;
            set
            {
                _categorySelectedItem = value;

                _pageNumber = 0;
                Items.Clear();
                SelectItemsSource();
                OnPropertyChanged();
            }
        }

        public CouponBrands BrandSelectedItem
        {
            get => _brandSelectedItem;
            set
            {
                _brandSelectedItem = value;

                _pageNumber = 0;
                Items.Clear();
                SelectItemsSource();
                OnPropertyChanged();
            }
        }

        private bool _categoryIsVisible;
        private bool _brandIsVisible;

        public bool CategoryIsVisible 
        { 
            get => _categoryIsVisible;
            set 
            {
                _categoryIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool BrandIsVisible
        { 
            get => _brandIsVisible;
            set 
            {
                _brandIsVisible = value;
                OnPropertyChanged();
            } 
        }

        public async Task Filter(string filter)
        {
            try
            {
                IsBusy = true;
                if (currentFilter == filter)
                    return;
                switch (filter)
                {
                    case "noFilter":
                        Items.Clear();
                        CategoryIsVisible = false;
                        BrandIsVisible = false;
                        await RefreshAsync();
                        break;
                    case "brandFilter":
                        CategoryIsVisible = false;
                        BrandIsVisible = true;
                        break;
                    case "categoriesFilter":
                        CategoryIsVisible = true;
                        BrandIsVisible = false;
                        break;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private bool CanExecute(object arg)
        {
            return !IsBusy;
        }

        private async Task RefreshAsync()
        {
            try
            {
                IsBusy = true;
                Items.Clear();
                _pageNumber = 0;
                await SelectItemsSource();
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
