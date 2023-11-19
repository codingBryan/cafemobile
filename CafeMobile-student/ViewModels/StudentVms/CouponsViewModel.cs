using CafeMobile.Models;
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CafeMobile.ViewModels.StudentVms
{
    public partial class CouponsViewModel:ObservableObject
    {
        [ObservableProperty]
        public IEnumerable<Coupon> coupons;
        public CouponsViewModel()
        {
            coupons = GetCoupons();
        }

        public IEnumerable<Coupon> GetCoupons()
        {
            return new List<Coupon> 
            {
                new Coupon
                {
                    CouponId=1,
                    name = "Breakfast coupon",
                    price = 1200,
                    image_url = "https://images.unsplash.com/photo-1533910534207-90f31029a78e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80",
                    is_active =true
                },
                new Coupon
                {
                    CouponId=2,
                    name = "Lunch coupon",
                    price = 1700,
                    image_url = "https://images.unsplash.com/photo-1533910534207-90f31029a78e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80",
                    is_active =true,
                },
                new Coupon
                {
                    CouponId=3,
                    name = "Gold Breakfast coupon",
                    price = 100,
                    image_url = "https://images.unsplash.com/photo-1533910534207-90f31029a78e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80",
                    is_active =true
                },
                new Coupon
                {
                    CouponId=1,
                    name = "Gold coupon",
                    price = 100,
                    image_url = "https://images.unsplash.com/photo-1533910534207-90f31029a78e?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80",
                    is_active =true
                },

            };

        }
        [RelayCommand]
        async Task GoHome()
        {
            await Shell.Current.GoToAsync(nameof(StudentMenu));
        }

        [RelayCommand]
        async Task GoToCouponDetails()
        {
            await Shell.Current.GoToAsync("CouponDetailsStudent");
        }
    }
}
