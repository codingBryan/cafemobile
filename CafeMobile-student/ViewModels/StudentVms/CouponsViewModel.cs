using CafeMobile.Models;
using CafeMobile.Models.DTOs;
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CafeMobile.ViewModels.StudentVms
{
    public partial class CouponsViewModel:BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<GetStudentCoupon> coupons;

        public CouponsViewModel()
        {
            
        }
        [RelayCommand]
        public async Task Init()
        {
            IsBusy = true;
            GetCoupons();
            IsBusy = false;
        }

        private async void GetCoupons()
        {
            CreateClient();
            var jwtToken = Preferences.Get("token", "defaultValue");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var response = await client.GetAsync("/api/student/myCoupons");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<ObservableCollection<GetStudentCoupon>> data = JsonConvert.DeserializeObject<Response<ObservableCollection<GetStudentCoupon>>>(response_string);
                Coupons = data.data;
            }

        }
        [RelayCommand]
        async Task GoHome()
        {
            await Shell.Current.GoToAsync(nameof(StudentMenu));
        }

        [RelayCommand]
        async Task GoToCouponDetails(GetStudentCoupon coupon)
        {
            await Shell.Current.GoToAsync(nameof(CouponDetails), new Dictionary<string, object>
            {
                ["coupon"] = coupon
            });
        }
    }
}
