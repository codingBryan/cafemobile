
using CafeMobile.Models;
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CafeMobile.ViewModels
{
    public partial class RedemptionsViewModel:BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<Redemption> redemptions = new();

        [RelayCommand]
        public async void Init()
        {
            
            FetchRedemtions();
        }

        [RelayCommand]
        async Task FetchRedemtions()
        {
            IsBusy = true;
            var jwtToken = Preferences.Get("token", "defaultValue");
            CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var response = await client.GetAsync("api/student/redemptions");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                var response_data = JsonConvert.DeserializeObject<Response<ObservableCollection<Redemption>>>(response_string);
                Redemptions = response_data.data;
            }
            IsBusy = false;
        }

        [RelayCommand]
        async Task RedemptionInfo(Redemption redemption)
        {
            await Shell.Current.GoToAsync(nameof(DisplayQRCode),new Dictionary<string, object>
            {
                ["redemption"] = redemption
            });
        }
    }
}
