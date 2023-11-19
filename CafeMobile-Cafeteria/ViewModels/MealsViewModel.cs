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

        private HttpClient client;
        private string baseUrl = "https://dcqv9t8r-7276.euw.devtunnels.ms/";
        private readonly IMapper mapper;

        public MealsViewModel(IMapper mapper)
        {
            client = new();
            client.BaseAddress = new Uri(baseUrl);
            this.mapper = mapper;
        }
        [RelayCommand]
        public async Task init()
        {
            GetMeals();
        }
        
        private async void GetMeals()
        {
            PageRefreshing = true;
            var response = await client.GetAsync("api/cafeteria/menu");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                var response_data = JsonConvert.DeserializeObject<Response<IEnumerable<GetMealDTO>>>(response_string);
                IEnumerable<MealDisplay> displayMenu = response_data.data.Select(m=>mapper.Map<MealDisplay>(m)).ToList();
                Meals = displayMenu;
              
               
            }
            foreach(var meal in Meals)
            {
                if(meal.image != null)
                {
                    meal.displayImage = ImageSource.FromStream(() => new MemoryStream(meal.image));
                }
                else
                {
                    meal.displayImage = "dotnet_bot";
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
