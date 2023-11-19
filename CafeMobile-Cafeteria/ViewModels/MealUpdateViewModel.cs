using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Models.System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cafemobile_Cafeteria.ViewModels
{
    [QueryProperty(nameof(Meal),"meal")]
    public partial class MealUpdateViewModel:BaseViewModel
    {

        [ObservableProperty]
        public MealDisplay meal;
        [RelayCommand]
        async Task UpdateMeal(Meal meal)
        {
            await Shell.Current.DisplayAlert("Success", "Meal updated", "OK");
        }
    }
}
