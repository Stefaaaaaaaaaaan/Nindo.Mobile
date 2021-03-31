using Xamarin.CommunityToolkit.ObjectModel;
using Nindo.Mobile.Services;
using Nindo.Net.Models;
using System.Threading.Tasks;
using Nindo.Common.Common;
using MvvmHelpers.Commands;
using AsyncCommand = Xamarin.CommunityToolkit.ObjectModel.AsyncCommand;
using System.Windows.Input;
using Xamarin.Essentials;

namespace Nindo.Mobile.ViewModels
{
    class CouponViewModel : ViewModelBase
    {
        #region command
        public IAsyncCommand RefreshCommand { get; }
        public IAsyncCommand LoadMoreCoupons { get; set; }
        public IAsyncCommand OnCollectionViewSelectionChanged { get; set; }

        public IAsyncCommand NoFilterCommand { get; }
        public ICommand BrandFilterCommand { get; }
        public ICommand CategoriesFilterCommand { get; }
        #endregion

        private readonly IApiService _apiService;
        private int page;


        public RangeObservableCollection<Coupon> Items { get; set; }

        public CouponViewModel(IApiService apiService)
        {
            Title = "Coupons";
            _apiService = apiService;

            Items = new RangeObservableCollection<Coupon>();
            BrandItems = new RangeObservableCollection<CouponBrands>();
            CategoryItems = new RangeObservableCollection<string>();

            LoadMoreCoupons = new AsyncCommand(LoadCoupons);
            RefreshCommand = new AsyncCommand(RefreshAsync, CanExecute);
            OnCollectionViewSelectionChanged = new AsyncCommand(CopyCouponCode);

            NoFilterCommand = new AsyncCommand(setNoFilter);
            CategoriesFilterCommand = new Command(setCategoryFilter);
            BrandFilterCommand = new Command(setBrandFilter);

        }

        public async Task LoadCoupons()
        {

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {

                var coupon = (await _apiService.GetCouponsAsync(page));
                if (coupon.HasMore == "true")
                {
                    Items.AddRange(coupon.Coupon);
                    page += 20;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task CategoryFilterCoupons()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var category = await _apiService.GetCouponsByCategoryAsync(CategorySelectedItem);
                Items.AddRange(category.Coupon);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task BrandFilterCoupons()
        {
            IsBusy = true;
            try
            {
                Items.Clear();
                var brand = await _apiService.GetCouponsByBranchAsync(_brandSelectedItem.Id);
                Items.AddRange(brand.Coupon);
            }
            finally
            {
                IsBusy = false;
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

        private RangeObservableCollection<string> _categoryItems;

        private RangeObservableCollection<CouponBrands> _brandItems;

        private string _categorySelectedItem;
        private CouponBrands _brandSelectedItem;
        private Coupon _collectionViewSelectedItem;

        public RangeObservableCollection<string> CategoryItems
        {
            get
            {
                return _categoryItems;
            }
            set
            {
                _categoryItems = value;
                OnPropertyChanged();
            }
        }


        public RangeObservableCollection<CouponBrands> BrandItems
        {
            get
            {
                return _brandItems;
            }
            set
            {
                _brandItems = value;
                OnPropertyChanged();
            }
        }

        public Coupon CollectionViewSelectedItem
        {
            get
            {
                return _collectionViewSelectedItem;
            }
            set
            {
                _collectionViewSelectedItem = value;

                OnPropertyChanged();
            }
        }

        public string CategorySelectedItem
        {
            get
            {
                return _categorySelectedItem;
            }
            set
            {
                _categorySelectedItem = value;

                CategoryFilterCoupons();
                OnPropertyChanged();
            }
        }

        public CouponBrands BrandSelectedItem
        {
            get
            {
                return _brandSelectedItem;
            }
            set
            {
                _brandSelectedItem = value;

                BrandFilterCoupons();
                OnPropertyChanged();
            }
        }

        private bool _categoryIsVisible;
        private bool _brandIsVisible;

        public bool CategoryIsVisible 
        { 
            get
            {
                return _categoryIsVisible;
            }
            set 
            {
                _categoryIsVisible = value;
                OnPropertyChanged();
            }
        }
        public bool BrandIsVisible
        { 
            get 
            {
                return _brandIsVisible;
            }
            set 
            {
                _brandIsVisible = value;
                OnPropertyChanged();
            } 
        }

        public async Task setNoFilter()
        {
            CategoryIsVisible = false;
            BrandIsVisible = false;

            object arg = null;
            if (CanExecute(arg) == true)
            {
                await RefreshAsync();
            }
        }

        public void setCategoryFilter()
        {
            CategoryIsVisible = true;
            BrandIsVisible = false;
        }

        public void setBrandFilter()
        {
            CategoryIsVisible = false;
            BrandIsVisible = true;
        }

        private bool CanExecute(object arg)
        {
            return !IsBusy && !IsRefreshing;
        }

        private async Task RefreshAsync()
        {
            IsBusy = true;
            try
            {
                IsRefreshing = true;
                Items.Clear();
                page = 0;
                await LoadCoupons();
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            }
        }
    }
}
