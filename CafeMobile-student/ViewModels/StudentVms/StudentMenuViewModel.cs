using CafeMobile.Models;
using CafeMobile.Models.DTOs;
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;

namespace CafeMobile.ViewModels.StudentVms
{
    [QueryProperty(nameof(Student),"student")]
    public partial class StudentMenuViewModel:BaseViewModel
    {
        private readonly CartViewModel cartViewModel;
        private readonly IMapper mapper;
        private HttpClient client;
        private string baseUrl = "https://dcqv9t8r-7276.euw.devtunnels.ms/";

        [ObservableProperty]
        public IEnumerable<Category> categories;

        [ObservableProperty]
        public bool cartHasItems = false;

        [ObservableProperty]
        public StudentInfo student;

        [ObservableProperty]
        public IEnumerable<MealDisplay> meals;
        [ObservableProperty]
        public bool flyoutIsVisible = false;

        [ObservableProperty]
        public int cartCount;


        public StudentMenuViewModel(CartViewModel cartViewModel,IMapper mapper)
        {

            this.mapper = mapper;
            client = new();
            client.BaseAddress = new Uri(baseUrl);
            Student = new StudentInfo();
            categories = fetchCategories();
            this.cartViewModel= cartViewModel;
        }

        public async void FetchMeals()
        {
            var response = await client.GetAsync("api/student/menu");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                var response_data = JsonConvert.DeserializeObject<Response<IEnumerable<GetMealDTO>>>(response_string);
                IEnumerable<MealDisplay> displayMenu = response_data.data.Select(m => mapper.Map<MealDisplay>(m)).ToList();
                Meals = displayMenu;


            }
            
            foreach (var meal in Meals)
            {
                if (meal.image != null)
                {
                    meal.displayImage = ImageSource.FromStream(() => new MemoryStream(meal.image));
                }
                else
                {
                    meal.displayImage = "dotnet_bot";
                }

            }
        }


        public IEnumerable<Category> fetchCategories()
        {
            return new List<Category>
            {
                new Category
                {
                    id = 1,
                    name = "beverages",
                    image_url = "https://thumbs.dreamstime.com/z/cans-beverages-19492376.jpg?w=768"
                },
                new Category
                {
                    id = 2,
                    name = "veges",
                    image_url = "https://images.unsplash.com/photo-1579113800032-c38bd7635818?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80"
                },
                new Category
                {
                    id = 3,
                    name = "snacks",
                    image_url = "https://images.unsplash.com/photo-1612773843298-44dcdd45d865?ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D&auto=format&fit=crop&w=1887&q=80"
                }
            };
        }

        [RelayCommand]
        async Task ToggleFlyOutVisibility()
        {
            FlyoutIsVisible = !FlyoutIsVisible;
        }
        [RelayCommand]
        async Task GoToCoupons()
        {
            FlyoutIsVisible = !FlyoutIsVisible;
            await Shell.Current.GoToAsync(nameof(Coupons));
            
        }
        [RelayCommand]
        async Task GoToCart()
        {
            await Shell.Current.GoToAsync(nameof(Cart));
        }

        [RelayCommand] 
        public void AddMealToCart(MealDisplay? meal)
        {
            
            if (meal is not null)
            {
                CartItem cartItem = new CartItem()
                {
                    MealId = meal.MealId,
                    name = meal.name,
                    price = meal.price,
                    price_CP = meal.price_CP,
                    quantity = 1,
                    displayImage = meal.displayImage
                };
                cartViewModel.AddMealToCart(cartItem);
                CartCount = cartViewModel.GetCartCount();
                if (CartCount != 0)
                {
                    CartHasItems = true;
                }
                else
                {
                    CartHasItems = false;
                }
            }

        }
    }   

    
}
