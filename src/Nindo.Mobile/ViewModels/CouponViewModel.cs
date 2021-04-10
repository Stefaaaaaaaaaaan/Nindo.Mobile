using Xamarin.CommunityToolkit.ObjectModel;
using Nindo.Mobile.Services;
using Nindo.Net.Models;
using System.Threading.Tasks;
using Nindo.Common.Common;
using AsyncCommand = Xamarin.CommunityToolkit.ObjectModel.AsyncCommand;
using Xamarin.Essentials;
using Syncfusion.XForms.ComboBox;
using System.Linq;

namespace Nindo.Mobile.ViewModels
{
    class CouponViewModel : ViewModelBase
    {
        #region command
        public IAsyncCommand RefreshCommand { get; }
        public IAsyncCommand LoadMoreCouponsCommand { get; set; }
        public IAsyncCommand CollectionViewSelectionChangedCommand { get; set; }
        public IAsyncCommand<object> CategorySelectionChangedCommand { get; set; }
        public IAsyncCommand<object> BrandSelectionChangedCommand { get; set; }
        public IAsyncCommand<string> FilterCommand { get; }
        #endregion

        private readonly IApiService _apiService;

        public RangeObservableCollection<Coupon> Items { get; set; }

        public CouponViewModel(IApiService apiService)
        {
            Title = "Coupons";
            _apiService = apiService;

            Items = new RangeObservableCollection<Coupon>();
            BrandItems = new RangeObservableCollection<CouponBrands>();
            CategoryItems = new RangeObservableCollection<string>();

            LoadMoreCouponsCommand = new AsyncCommand(SelectItemsSource, CanExecute);
            RefreshCommand = new AsyncCommand(RefreshAsync, CanExecute);
            CollectionViewSelectionChangedCommand = new AsyncCommand(CopyCouponCode, CanExecute);
            CategorySelectionChangedCommand = new AsyncCommand<object>(CategorySelectionChangedAsync, CanExecute);
            BrandSelectionChangedCommand = new AsyncCommand<object>(BrandSelectionChangedAsync, CanExecute);

            FilterCommand = new AsyncCommand<string>(Filter, CanExecute);
        }


        private int _pageNumber;
        bool hasMore = true;
        public async Task SelectItemsSource()
        {

            try
            {
                IsBusy = true;

                if (CategoryIsVisible == false && BrandIsVisible == false && hasMore == true)
                {
                    var coupon = await _apiService.GetCouponsAsync(_pageNumber);
                    Items.AddRange(coupon.Coupon);
                    if (coupon.HasMore == "true")
                    {
                        _pageNumber += 20;
                    }
                    else
                    {
                        hasMore = false;
                    }
                }
                else if (CategoryIsVisible == false && BrandIsVisible == true && hasMore == true)
                {
                    var brand = await _apiService.GetCouponsByBranchAsync(BrandSelectedItem.Id, _pageNumber);
                    Items.AddRange(brand.Coupon);
                    if (brand.HasMore == "true")
                    {
                        _pageNumber += 20;
                    }
                    else
                    {
                        hasMore = false;
                    }
                }
                else if (CategoryIsVisible == true && BrandIsVisible == false && hasMore == true)
                {
                    var category = _apiService.GetCouponsByCategoryAsync(CategorySelectedItem, _pageNumber).Result;
                    Items.AddRange(category.Coupon);
                    if (category.HasMore == "true")
                    {
                        _pageNumber += 20;
                    }
                    else
                    {
                        hasMore = false;
                    }
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task CategorySelectionChangedAsync(object obj)
        {
            try
            {
                IsBusy = true;
                var selectionChangedArgs = obj as SelectionChangedEventArgs;
                if (CategoryItems.Contains(selectionChangedArgs.Value))
                {
                    Items.Clear();
                    _pageNumber = 0;
                    await SelectItemsSource();
                }
                else
                {
                    return;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task BrandSelectionChangedAsync(object obj)
        {
            try
            {
                IsBusy = true;
                var selectionChangedArgs = obj as SelectionChangedEventArgs;
                if (BrandItems.Contains(selectionChangedArgs.Value))
                {
                    Items.Clear();
                    _pageNumber = 0;
                    await SelectItemsSource();
                }
                else
                {
                    return;
                }
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
        public RangeObservableCollection<string> CategoryItems
        {
            get => _categoryItems;
            set
            {
                _categoryItems = value;
                OnPropertyChanged();
            }
        }

        private RangeObservableCollection<CouponBrands> _brandItems;
        public RangeObservableCollection<CouponBrands> BrandItems
        {
            get => _brandItems;
            set
            {
                _brandItems = value;
                OnPropertyChanged();
            }
        }


        private Coupon _collectionViewSelectedItem;
        public Coupon CollectionViewSelectedItem
        {
            get => _collectionViewSelectedItem;
            set
            {
                _collectionViewSelectedItem = value;

                OnPropertyChanged();
            }
        }

        private string _categorySelectedItem;
        public string CategorySelectedItem
        {
            get => _categorySelectedItem;
            set
            {
                _categorySelectedItem = value;

                OnPropertyChanged();
            }
        }

        private CouponBrands _brandSelectedItem;
        public CouponBrands BrandSelectedItem
        {
            get => _brandSelectedItem;
            set
            {
                _brandSelectedItem = value;

                OnPropertyChanged();
            }
        }

        private bool _categoryIsVisible;

        public bool CategoryIsVisible
        {
            get => _categoryIsVisible;
            set
            {
                _categoryIsVisible = value;
                OnPropertyChanged();
            }
        }

        private bool _brandIsVisible;
        public bool BrandIsVisible
        {
            get => _brandIsVisible;
            set
            {
                _brandIsVisible = value;
                OnPropertyChanged();
            }
        }

        private string currentFilter;
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

                        hasMore = true;
                        CategoryIsVisible = false;
                        BrandIsVisible = false;
                        await RefreshAsync();
                        currentFilter = "noFilter";
                        break;
                    case "brandFilter":

                        hasMore = true;
                        BrandSelectedItem = BrandItems.First();
                        CategoryIsVisible = false;
                        BrandIsVisible = true;
                        await RefreshAsync();
                        currentFilter = "brandFilter";
                        break;
                    case "categoriesFilter":

                        hasMore = true;
                        CategorySelectedItem = CategoryItems.First();
                        CategoryIsVisible = true;
                        BrandIsVisible = false;
                        await RefreshAsync();
                        currentFilter = "categoriesFilter";
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
