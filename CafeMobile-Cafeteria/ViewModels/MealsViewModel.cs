using AutoMapper;
using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class MealsViewModel:BaseViewModel
    {
        [ObservableProperty]
        public IEnumerable<MealDisplay> meals;
        [ObservableProperty]
        public bool pageRefreshing = false;
        private readonly IMapper mapper;

        public MealsViewModel(IMapper mapper)
        {
            this.mapper = mapper;
            Meals = new List<MealDisplay>();
        }
        [RelayCommand]
        public async Task init()
        {
            GetMeals();
        }
        private async void GetMeals()
        {
            CreateClient();
            PageRefreshing = true;
            var response = await client.GetAsync("api/cafeteria/menu");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                var response_data = JsonConvert.DeserializeObject<Response<IEnumerable<GetMealDTO>>>(response_string);
                IEnumerable<MealDisplay> displayMenu = response_data.data.Select(m=>mapper.Map<MealDisplay>(m)).ToList();
                Meals = displayMenu;
                foreach (MealDisplay m in Meals)
                {
                    if (m.image == null)
                    {
                        m.image = "dotnet_bot";
                    }
                }
            }
            PageRefreshing = false;
            
        }
        [RelayCommand]
        async Task MealUpdate(MealDisplay meal)
        {
            await Shell.Current.GoToAsync(nameof(MealUpdate), new Dictionary<string, object>
            {
                ["meal"]=meal
            });
        }

        [RelayCommand]
        async Task GotoAddMeal()
        {
            await Shell.Current.GoToAsync(nameof(CreateMeal));
        }
    }
}
