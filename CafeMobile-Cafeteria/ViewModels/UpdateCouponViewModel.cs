using AutoMapper;
using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Models.System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;

namespace Cafemobile_Cafeteria.ViewModels
{
    [QueryProperty(nameof(Coupon),"coupon")]
    public partial class UpdateCouponViewModel:BaseViewModel
    {
        [ObservableProperty]
        public bool mealSelectionIsVisible = false;

        [ObservableProperty]
        public int mealCount = 0;
        [ObservableProperty]
        public IEnumerable<MealDisplay> menu = new List<MealDisplay>();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(couponItems))]
        public CouponDisplay coupon;
        private readonly IMapper mapper;

        public IEnumerable<GetMealDTO> couponItems => Coupon.meals; 


        public UpdateCouponViewModel(IMapper mapper)
        {
            this.mapper = mapper;
            Coupon = new();
            GetMeals();
        }
        [RelayCommand]
        async Task DisplayMeals()
        {
            MealSelectionIsVisible = !MealSelectionIsVisible;
        }
        private async void GetMeals()
        {
            if (Menu.Count() == 0)
            {
                CreateClient();
                var response = await client.GetAsync("api/cafeteria/menu");
                if (response.IsSuccessStatusCode)
                {
                    var response_string = await response.Content.ReadAsStringAsync();
                    var response_data = JsonConvert.DeserializeObject<Response<IEnumerable<GetMealDTO>>>(response_string);
                    IEnumerable<MealDisplay> displayMenu = response_data.data.Select(m => mapper.Map<MealDisplay>(m)).ToList();
                    Menu = displayMenu.Select(m => mapper.Map<MealDisplay>(m)).ToList();
                }
                foreach (var meal in Menu)
                {
                    if (meal.image == null)
                    {
                        meal.image = "dotnet_bot";
                    }
                    

                }
            }
        }
            [RelayCommand]
            async Task AddSelectedMeals()
            {
                var selectedItems = Menu.Where(m => m.isSelected).ToList();
                foreach (var meal in selectedItems)
                {
                    GetMealDTO mealDTO = mapper.Map<GetMealDTO>(meal);
                    if (!Coupon.meals.Contains(mealDTO))
                    {
                        Coupon.meals.Append(mealDTO);
                    }
                }
                MealCount = selectedItems.Count();
                MealSelectionIsVisible = !MealSelectionIsVisible;
            }

            [RelayCommand]
            async Task UpdateCoupon()
            {
                IsBusy = true;
                NewCouponDTO updateCoupon = mapper.Map<NewCouponDTO>(Coupon);
                updateCoupon.meal_Ids = new List<int>();
                foreach (var meal in Menu)
                {
                    if (meal.isSelected)
                    {
                        updateCoupon.meal_Ids.Add(meal.MealId);
                    }

                };
                var jwtToken = Preferences.Get("token", "defaultValue");
                var json = JsonConvert.SerializeObject(updateCoupon);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
                var response = await client.PostAsync("/api/cafeteria/CreateCoupon", content);
                if (response.IsSuccessStatusCode)
                {
                    var response_string = await response.Content.ReadAsStringAsync();
                    Response<GetCouponDTO> data = JsonConvert.DeserializeObject<Response<GetCouponDTO>>(response_string);
                    if (data.data != null)
                    {
                        await Shell.Current.DisplayAlert("Success", "Coupon updated", "Ok");
                        await Shell.Current.GoToAsync("..");
                        MealCount = 0;
                        IsBusy = false;
                    }
                }
                else
                {
                    await Shell.Current.DisplayAlert("Failed", "Failed to update coupon", "Ok");
                }
            }
        }
    
}
