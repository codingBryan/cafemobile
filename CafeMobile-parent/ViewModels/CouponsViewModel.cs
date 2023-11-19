using CafeMobile_parent.Models.DTO;
using CafeMobile_parent.Models.System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CafeMobile_parent.ViewModels
{
    [QueryProperty(nameof(Student), "student")]
    public partial class CouponsViewModel:BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<GetCouponDTO> coupons;
        [ObservableProperty]
        public StudentInfo student;

        public CouponsViewModel()
        {
            
        }
        [RelayCommand]
        async Task CouponDetails(GetCouponDTO coupon)
        {
            await Shell.Current.GoToAsync(nameof(CouponDetails),
                new Dictionary<string, object>
                {
                    ["coupon"] = coupon,
                    ["student"] = Student
                });
        }
        [RelayCommand]
        public async Task Init()
        {
            IsRefresh = true;
            FetchCoupons();
            IsRefresh = false;
        }

        private async void FetchCoupons()
        {
            CreateClient();
            var response = await client.GetAsync($"/api/parent/fetchcoupons");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<ObservableCollection<GetCouponDTO>> data = JsonConvert.DeserializeObject<Response<ObservableCollection<GetCouponDTO>>>(response_string);
                if (data.data != null)
                {
                    Coupons = data.data;
                }
            }

        }
    }
}
