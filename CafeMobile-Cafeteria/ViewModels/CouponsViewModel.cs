using AutoMapper;
using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Models.System;
using Cafemobile_Cafeteria.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class CouponsViewModel:BaseViewModel
    {

        [ObservableProperty]
        public IEnumerable<CouponDisplay> coupons;

        private readonly IMapper mapper;
        public CouponsViewModel(IMapper mapper)
        {
            this.mapper = mapper;
        }
        [RelayCommand]
        public async Task Init()
        {
            GetCoupons();
        }
        private async void GetCoupons()
        {
            CreateClient();
            IsBusy = true;
            var response = await client.GetAsync("api/cafeteria/fetchCoupons");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                var response_data = JsonConvert.DeserializeObject<Response<IEnumerable<GetCouponDTO>>>(response_string);
                Coupons = response_data.data.Select(c=>mapper.Map<CouponDisplay>(c)).ToList();
            }
            foreach (var coupon in Coupons)
            {
                if (coupon.image != null)
                {
                    coupon.displayImage = ImageSource.FromStream(() => new MemoryStream(coupon.image));
                }
                else
                {
                    coupon.displayImage = "dotnet_bot";
                }

            }
            IsBusy = false;
        }
        [RelayCommand]
        async Task GoHome()
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        async Task CouponUpdate(CouponDisplay coupon)
        {
            await Shell.Current.GoToAsync(nameof(UpdateCoupon), new Dictionary<string, object>
            {
                ["coupon"] = coupon
            });
        }

        [RelayCommand]
        async Task GoToAddCoupons()
        {
            await Shell.Current.GoToAsync(nameof(CreateCoupon));
        }
    }
}
