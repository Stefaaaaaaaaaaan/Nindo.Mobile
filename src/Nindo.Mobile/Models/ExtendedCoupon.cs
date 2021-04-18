using Nindo.Common.Common;
using Nindo.Mobile.ViewModels;
using Nindo.Net.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nindo.Mobile.Models
{
    class ExtendedCoupon<T> : ViewModelBase
    {
        public string CouponTitle { get; set; }

        private RangeObservableCollection<Coupon> _coupons = new RangeObservableCollection<Coupon>();

        public RangeObservableCollection<Coupon> Coupons
        {
            get => _coupons;
            set
            {
                _coupons = value;
                OnPropertyChanged();
            }
        }

        private RangeObservableCollection<T> _comboboxItems;
        public RangeObservableCollection<T> ComboboxItems
        {
            get => _comboboxItems;
            set
            {
                _comboboxItems = value;
                OnPropertyChanged();
            }
        }
    }
}
