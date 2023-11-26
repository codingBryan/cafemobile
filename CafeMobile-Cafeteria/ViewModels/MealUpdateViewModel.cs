using AutoMapper;
using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace Cafemobile_Cafeteria.ViewModels
{
    [QueryProperty(nameof(Meal),"meal")]
    public partial class MealUpdateViewModel:BaseViewModel
    {
        private readonly IMapper mapper;
        [ObservableProperty]
        public MealDisplay meal;
        public MealUpdateViewModel(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [RelayCommand]
        async Task UpdateMeal(MealDisplay meal)
        {
            NewMealDTO updateMeal = mapper.Map<NewMealDTO>(meal);
            CreateClient();
            var jwtToken = Preferences.Get("token", "defaultValue");
            var json = JsonConvert.SerializeObject(meal);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var response = await client.PostAsync("/api/cafeteria/CreateMeal", content);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<GetMealDTO> data = JsonConvert.DeserializeObject<Response<GetMealDTO>>(response_string);
                if (data.success == true)
                {
                    await Shell.Current.DisplayAlert("Success", "Meal Updated", "OK");
                    await Shell.Current.GoToAsync("..");
                }
            }
        }

        
    }
}
