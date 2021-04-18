using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nindo.Mobile.Models;
using Nindo.Mobile.Services;
using Nindo.Net.Models;
using Xamarin.CommunityToolkit.ObjectModel;
using Nindo.Common.Common;
using Xamarin.Forms.Internals;

namespace Nindo.Mobile.ViewModels
{
    class CouponViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        #region command
        public IAsyncCommand RefreshCommand { get; }
        public IAsyncCommand LoadMoreCouponsCommand { get; set; }
        public IAsyncCommand CollectionViewSelectionChangedCommand { get; set; }
        public IAsyncCommand<object> ComboboxSelectionChangedCommand { get; set; }

        #endregion

        public CouponViewModel(IApiService apiService)
        {
            Coupons = new[]
            {
                new ExtendedCoupon<object>
                {
                    CouponTitle = "Keine Filter"
                },
                new ExtendedCoupon<object>
                {
                    CouponTitle = "Brand Filter"
                },
                new ExtendedCoupon<object>
                {
                    CouponTitle = "Category Filter"
                }
            };

            _apiService = apiService; 
            RefreshCommand = new AsyncCommand(RefreshAsync, CanExecute);
        }

        public void InitLists()
        {
            Coupons[0].Coupons = new RangeObservableCollection<Coupon>();
            Coupons[1].Coupons = new RangeObservableCollection<Coupon>();
            Coupons[2].Coupons = new RangeObservableCollection<Coupon>();

            Coupons[1].ComboboxItems = new RangeObservableCollection<object>();
            Coupons[2].ComboboxItems = new RangeObservableCollection<object>();
        }

        private int _pageNumber;
        bool hasMore = true;
        public async Task LoadCouponsAsync()
        {
            try
            {
                IsBusy = true;

                await Task.Run(async () =>
                {
                    switch (TabViewSelectedIndex)
                    {
                        case 0:
                            var coupon = await _apiService.GetCouponsAsync(_pageNumber);
                            Coupons[0].Coupons.AddRange(coupon.Coupon);
                            if (coupon.HasMore == "true")
                            {
                                _pageNumber += 20;
                            }
                            else
                            {
                                hasMore = false;
                            }
                            break;
                        /*case 1:
                            var brand = await _apiService.GetCouponsByBranchAsync(BrandSelectedItem.Id, _pageNumber);
                            Coupons[1].Coupons.AddRange(brand.Coupon);
                            if (brand.HasMore == "true")
                            {
                                _pageNumber += 20;
                            }
                            else
                            {
                                hasMore = false;
                            }
                            break;
                        case 2:
                            var category = _apiService.GetCouponsByCategoryAsync(CategorySelectedItem, _pageNumber).Result;
                            Coupons[2].Coupons.AddRange(category.Coupon);
                            if (category.HasMore == "true")
                            {
                                _pageNumber += 20;
                            }
                            else
                            {
                                hasMore = false;
                            }
                            break;*/
                    }
                });
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task LoadBrands()
        {
            var brands = await _apiService.GetCouponBrandsAsync();
            Coupons[1].ComboboxItems.AddRange(brands);
        }
        public async Task LoadCategorys()
        {
            var categorys = await _apiService.GetCouponBranchesAsync();
            Coupons[2].ComboboxItems.AddRange(categorys);
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

        private IList<ExtendedCoupon<object>> _coupons;

        public IList<ExtendedCoupon<object>> Coupons
        {
            get => _coupons;
            set
            {
                _coupons = value;
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

        private int _tabViewSelectedIndex;

        public int TabViewSelectedIndex
        {
            get => _tabViewSelectedIndex;
            set
            {
                _tabViewSelectedIndex = value;
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
