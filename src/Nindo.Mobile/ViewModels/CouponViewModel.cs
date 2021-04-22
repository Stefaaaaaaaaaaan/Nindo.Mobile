using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nindo.Common.Common;
using Nindo.Mobile.Models;
using Nindo.Mobile.Services;
using Nindo.Net.Models;
using Xamarin.CommunityToolkit.ObjectModel;

namespace Nindo.Mobile.ViewModels
{
    public class CouponViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        #region command
        public IAsyncCommand RefreshCommand { get; }
        public IAsyncCommand LoadMoreCouponsCommand { get; set; }
        public IAsyncCommand ComboboxSelectionChangedCommand { get; set; }
        #endregion

        public CouponViewModel(IApiService apiService)
        {
            Coupons = new[]
            {
                new ExtendedCoupon
                {
                    CouponTitle = "keine Filter"
                },
                new ExtendedCoupon
                {
                    CouponTitle = "Brand Filter"
                },
                new ExtendedCoupon
                {
                    CouponTitle = "Category Filter"
                }
            };

            InitLists();
            _apiService = apiService;
            RefreshCommand = new AsyncCommand(RefreshAsync, CanExecute);
            LoadMoreCouponsCommand = new AsyncCommand(LoadCouponsAsync, CanExecute);
            ComboboxSelectionChangedCommand = new AsyncCommand(ComboboxSelectionChangedAsync, CanExecute);
        }

        public void InitLists()
        {
            Coupons[0].Coupons = new RangeObservableCollection<Coupon>() { };
            Coupons[1].Coupons = new RangeObservableCollection<Coupon>() { };
            Coupons[2].Coupons = new RangeObservableCollection<Coupon>() { };

            Coupons[0].ComboboxIsVisible = false;
            Coupons[1].ComboboxIsVisible = true;
            Coupons[2].ComboboxIsVisible = true;
        }

        public async Task LoadComboboxItemsAsync()
        {
            var brandItems = await _apiService.GetCouponBrandsAsync();
            var categories = await _apiService.GetCouponBranchesAsync();

            RangeObservableCollection<CouponBrands> categoryItems = new RangeObservableCollection<CouponBrands>();
            foreach (var item in categories)
            {
                categoryItems.Add(new CouponBrands { Name = item });
            }

            Coupons[1].ComboboxItems.AddRange(brandItems);
            Coupons[2].ComboboxItems.AddRange(categoryItems);
        }

        public async Task ComboboxSelectionChangedAsync()
        {
            try
            {
                IsBusy = true;
                if (ComboboxSelectedItem != null) {
                    await LoadCouponsAsync();
                    _pageNumber = 0;
                    hasMore = true;
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

        private bool hasMore = true;
        int _pageNumber;
        public async Task LoadCouponsAsync()
        {
            try
            {
                IsBusy = true;

                await Task.Run(async () =>
                {

                    switch (SelectedTabIndex) {
                        case 0:
                            var noFilter = await _apiService.GetCouponsAsync(_pageNumber);
                            Coupons[0].Coupons.AddRange(noFilter.Coupon.ToList());
                            if (noFilter.HasMore == "true")
                            {
                                _pageNumber += 20;
                            }
                            else
                            {
                                hasMore = false;
                            }
                            break;

                        case 1:
                            var brandFilter = await _apiService.GetCouponsByBranchAsync(ComboboxSelectedItem.Id, _pageNumber);
                            Coupons[1].Coupons.AddRange(brandFilter.Coupon.ToList());
                            if (brandFilter.HasMore == "true")
                            {
                                _pageNumber += 20;
                            }
                            else
                            {
                                hasMore = false;
                            }
                            break;

                        case 2:
                            var categoryFilter = await _apiService.GetCouponsByCategoryAsync(ComboboxSelectedItem.Name, _pageNumber);
                            Coupons[2].Coupons.AddRange(categoryFilter.Coupon.ToList());
                            if (categoryFilter.HasMore == "true")
                            {
                                _pageNumber += 20;
                            }
                            else
                            {
                                hasMore = false;
                            }
                            break;
                    }
                });
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task RefreshAsync()
        {
            try
            {
                IsRefreshing = true;

                await LoadCouponsAsync();
            }
            finally
            {
                IsRefreshing = false;
            }
        }

        private bool CanExecute(object arg)
        {
            return !IsBusy && !IsRefreshing;
        }

        private IList<ExtendedCoupon> _coupons;

        public IList<ExtendedCoupon> Coupons
        {
            get => _coupons;
            set
            {
                _coupons = value;
                OnPropertyChanged();
            }
        }

        private CouponBrands _comboboxSelectedItem;

        public CouponBrands ComboboxSelectedItem
        {
            get => _comboboxSelectedItem;
            set
            {
                _comboboxSelectedItem = value;
                OnPropertyChanged();
            }
        }

        private int _selectedTabIndex;

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged();
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
