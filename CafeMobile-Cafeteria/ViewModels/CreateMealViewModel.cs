using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class CreateMealViewModel:BaseViewModel
    {
        [ObservableProperty]
        public NewMealDTO meal;
        [ObservableProperty]
        public ImageSource imageSource;

        private byte[] selectedImageBytes;
        public CreateMealViewModel()
        {
            Meal = new();
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
                        Meal.image = imageData;
                        ImageSource = ImageSource.FromStream(() => new MemoryStream(imageData));
                    }
                }

            }
        }
        
        [RelayCommand]
        async Task CreateMeal(NewMealDTO meal)
        {
            CreateClient();
            var jwtToken = Preferences.Get("token","defaultValue");
            var json = JsonConvert.SerializeObject(meal);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var response = await client.PostAsync("/api/cafeteria/CreateMeal", content);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<GetMealDTO> data = JsonConvert.DeserializeObject<Response<GetMealDTO>>(response_string);
                if (data.data != null)
                {
                    await Shell.Current.DisplayAlert("Success", "Meal Created", "Ok");
                    await Shell.Current.GoToAsync("..");
                }
            }
        }
    }
}
