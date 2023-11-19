using AutoMapper;
using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Models.System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        public IEnumerable<Meal> menu = new List<Meal>(){};
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(couponItems))]
        public CouponDisplay coupon;
        private readonly IMapper mapper;

        public IEnumerable<GetMealDTO> couponItems => Coupon.meals; 


        public UpdateCouponViewModel(IMapper mapper)
        {
            this.mapper = mapper;
            Coupon = new();
        }
        [RelayCommand]
        async Task DisplayMeals()
        {
            MealSelectionIsVisible = !MealSelectionIsVisible;
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
            await Shell.Current.DisplayAlert("Created", "Coupon was updated successfully", "OK");
        }
    }
}
