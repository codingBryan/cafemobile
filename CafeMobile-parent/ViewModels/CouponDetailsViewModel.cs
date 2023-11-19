using CafeMobile_parent.Models.DTO;
using CafeMobile_parent.Models.System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace CafeMobile_parent.ViewModels
{
    [QueryProperty(nameof(Coupon),"coupon")]
    [QueryProperty(nameof(Student),"student")]
    public partial class CouponDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        public GetCouponDTO coupon;

        [ObservableProperty]
        public StudentInfo student;


        [RelayCommand]
        async Task BuyCoupon()
        {
            BuyCouponDTO cps = new()
            {
                CouponId = Coupon.CouponId,
                studentId = Student.StudentId,
                price = Coupon.price
            };
            CreateClient();
            var json = JsonConvert.SerializeObject(cps);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/parent/purchaseCoupon", content);
            if (response.IsSuccessStatusCode)
            {
                CredsNotSent = true;
                var response_string = await response.Content.ReadAsStringAsync();
                Response<string> data = JsonConvert.DeserializeObject<Response<string>>(response_string);
                if (data.data != null)
                {

                    await Shell.Current.DisplayAlert("Success", "Coupon purchase was successfull", "OK");
                    await Shell.Current.GoToAsync("../..");
                }
            }
        }
    }

        
    
}
