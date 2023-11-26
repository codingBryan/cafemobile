using CafeMobile.Models;
using CafeMobile.Models.DTOs;
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace CafeMobile.ViewModels.StudentVms
{
    [QueryProperty(nameof(Coupon),"coupon")]
    public partial class CouponDetailsViewModel: BaseViewModel
    {
        private readonly CartViewModel cartViewModel;
        private readonly IMapper mapper;
        [ObservableProperty]
        public GetStudentCoupon coupon;

        [ObservableProperty]
        public IEnumerable<MealDisplay> meals;

        [ObservableProperty]
        public int cartCount;
        public CouponDetailsViewModel(CartViewModel cartViewModel,IMapper mapper)
        {
            Coupon = new();
            Meals = new List<MealDisplay>();
            this.cartViewModel = cartViewModel;
            this.mapper = mapper;
        }
        [RelayCommand]
        async Task RedeemCoupon()
        {
            Processing = true;
            CreateClient();
            CouponRedemption redemption = new();
            IEnumerable<RedeemMealDTO> meals = new List<RedeemMealDTO>();
            foreach (MealDisplay meal in Meals)
            {
                if(meal.isSelected)
                {
                    redemption.redemptionTotal += meal.price_CP;
                    var mealDto=mapper.Map<RedeemMealDTO>(meal);
                    meals = meals.Append(mealDto);
                }
            }
            redemption.CouponId = Coupon.Id;
            redemption.meals = meals;
            Response<Redemption> res = new();
            var jwtToken = Preferences.Get("token", "defaultValue");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var json = JsonConvert.SerializeObject(redemption);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/student/redeemCoupon", content);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<Response<Redemption>>(responseString);
                if (res.success)
                {
                    Processing = false;
                    await Shell.Current.DisplayAlert("Success", "Redemption was successful", "OK");
                    await Shell.Current.GoToAsync(nameof(Redemptions));
                }
            }
        }
    }
}
