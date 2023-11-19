using AutoMapper;
using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Text;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class CreateCouponViewModel:BaseViewModel
    {
        [ObservableProperty]
        public NewCouponDTO coupon;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(isNotLoadingMenu))]
        public bool loadingMenu = true;
        public bool isNotLoadingMenu => !LoadingMenu;
        [ObservableProperty]
        public ImageSource imageSource;

        [ObservableProperty]
        public bool mealSelectionIsVisible = false;

        [ObservableProperty]
        public IEnumerable<MealDisplay> menu;

        [ObservableProperty]
        public ObservableCollection<MealDisplay> selectedMeals;

        [ObservableProperty]
        public int mealCount = 0;
        private readonly IMapper mapper;
        [RelayCommand]
        public async Task Init()
        {
            GetMeals();
        }
        
        public CreateCouponViewModel(IMapper mapper)
        {
            this.mapper = mapper;
            Coupon = new();
            SelectedMeals = new();
            Menu = new List<MealDisplay>();
        }

        [RelayCommand]
        async Task AddSelectedMeals()
        {
            var selectedItems = Menu.Where(m => m.isSelected).ToList();
            foreach (var meal in selectedItems)
            {
                if (!SelectedMeals.Contains(meal))
                {
                    SelectedMeals.Add(meal);
                    
                }

            }
            MealCount = selectedMeals.Count;
            MealSelectionIsVisible = !MealSelectionIsVisible;
        }

        [RelayCommand]
        async Task BrowsePhoto()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Select image"
            });

            if (result != null)
            {
                using (var stream = await result.OpenReadAsync())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        byte[] imageData = memoryStream.ToArray();
                        Coupon.image = imageData;
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(imageData));
                    }
                }

            }
        }
        private async void GetMeals()
        {
            if(Menu.Count() == 0)
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
                    if (meal.image != null)
                    {
                        meal.displayImage = ImageSource.FromStream(() => new MemoryStream(meal.image));
                    }
                    else
                    {
                        meal.displayImage = "dotnet_bot";
                    }

                }
                LoadingMenu = false;
            }
            
        }

        [RelayCommand]
        async Task CreateCoupon(NewCouponDTO coupon)
        {
            coupon.meal_Ids = new List<int>();
            foreach(var meal in SelectedMeals)
            {
               coupon.meal_Ids.Add(meal.MealId);
            };
            var jwtToken = Preferences.Get("token", "defaultValue");
            var json = JsonConvert.SerializeObject(coupon);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var response = await client.PostAsync("/api/cafeteria/CreateCoupon", content);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<GetCouponDTO> data = JsonConvert.DeserializeObject<Response<GetCouponDTO>>(response_string);
                if (data.data != null)
                {
                    await Shell.Current.DisplayAlert("Success", "Coupon Created", "Ok");
                    await Shell.Current.GoToAsync("..");
                    MealCount = 0;
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Failed", "Failed to create coupon", "Ok");
            }
        }

        [RelayCommand]
        async Task DisplayMeals()
        {
            MealSelectionIsVisible = !MealSelectionIsVisible;
        }
    }
}

