using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Nindo.Common.Common;
using Nindo.Mobile.Models;
using Nindo.Mobile.Services;
using Nindo.Net.Models;
using Syncfusion.XForms.ComboBox;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Nindo.Mobile.ViewModels
{
    public class CouponViewModel : ViewModelBase
    {
        private readonly IApiService _apiService;

        #region command
        public IAsyncCommand RefreshCommand { get; }
        public IAsyncCommand LoadMoreCouponsCommand { get; set; }
        public IAsyncCommand<CouponBrands> ComboboxSelectionChangedCommand { get; set; }
        public IAsyncCommand CollectionViewSelectionChangedCommand { get; set; }
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
            ComboboxSelectionChangedCommand = new AsyncCommand<CouponBrands>(ComboboxSelectionChangedAsync, CanExecute);
            CollectionViewSelectionChangedCommand = new AsyncCommand(CopyCouponCode, CanExecute);
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

        public async Task LoadBrandItemsAsync()
        {
            if (!Coupons[1].ComboboxItems.Any())
            {
                var brandItems = await _apiService.GetCouponBrandsAsync();

                Device.BeginInvokeOnMainThread(() =>
                {
                    Coupons[1].ComboboxItems.AddRange(brandItems);
                });
            }
        }

        public async Task LoadCategoryItemsAsync()
        {
            if (!Coupons[2].ComboboxItems.Any()) {
                var categories = await _apiService.GetCouponBranchesAsync();

                RangeObservableCollection<CouponBrands> categoryItems = new RangeObservableCollection<CouponBrands>();
                foreach (var item in categories)
                {
                    categoryItems.Add(new CouponBrands { Name = item });
                }

                Device.BeginInvokeOnMainThread(() =>
                {
                    Coupons[2].ComboboxItems.AddRange(categoryItems);
                });
            }
        }

        CouponBrands selectedItem;
        public async Task ComboboxSelectionChangedAsync(CouponBrands obj)
        {
            try
            {
                switch (SelectedTabIndex) {
                    case 1:
                    if (Coupons[1].ComboboxItems.Contains(obj))
                    {
                        Coupons[1].Coupons.Clear();
                        _pageNumber = 0;
                        hasMore = true;
                        selectedItem = obj;
                        await LoadCouponsAsync();
                    }
                    else
                    {
                        return;
                    }
                        break;

                    case 2:
                        if (Coupons[2].ComboboxItems.Contains(obj))
                        {
                            Coupons[2].Coupons.Clear();
                            _pageNumber = 0;
                            hasMore = true;
                            selectedItem = obj;
                            await LoadCouponsAsync();
                        }
                        else
                        {
                            return;
                        }
                        break;
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
                            if (hasMore == true) 
                            {
                                var noFilter = await _apiService.GetCouponsAsync(_pageNumber);
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Coupons[0].Coupons.AddRange(noFilter.Coupon);
                                });
                                if (noFilter.HasMore == "true")
                                {
                                    _pageNumber += 20;
                                }
                                else
                                {
                                    hasMore = false;
                                } 
                            }
                            break;

                        case 1:
                            if (hasMore == true)
                            {
                                var brandFilter = await _apiService.GetCouponsByBranchAsync(selectedItem.Id, _pageNumber);
                                Device.BeginInvokeOnMainThread(() =>
                                {
                                    Coupons[1].Coupons.AddRange(brandFilter.Coupon);
                                });
                                if (brandFilter.HasMore == "true")
                                {
                                    _pageNumber += 20;
                                }
                                else
                                {
                                    hasMore = false;
                                }
                            }
                            break;

                        case 2:
                            if (hasMore == true)
                            {
                                try
                                {
                                    var categoryFilter = await _apiService.GetCouponsByCategoryAsync(selectedItem.Name, _pageNumber);
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        Coupons[2].Coupons.AddRange(categoryFilter.Coupon);
                                    });
                                    if (categoryFilter.HasMore == "true")
                                    {
                                        _pageNumber += 20;
                                    }
                                    else
                                    {
                                        hasMore = false;
                                    }

                                }
                                catch(Exception e)
                                {
                                    Debug.WriteLine(e);
                                }
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

        public async Task CopyCouponCode()
        {
            await Clipboard.SetTextAsync(CollectionViewSelectedItem.Code);
            await Application.Current.MainPage.DisplayAlert("", "Code has been Copied", "UwU");
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
