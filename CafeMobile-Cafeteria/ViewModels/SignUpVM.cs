using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class SignUpVM:BaseViewModel
    {
        private string baseUrl = "https://dcqv9t8r-7276.euw.devtunnels.ms/";
        private HttpClient client;
        [ObservableProperty]
        public SignUpCreds creds;
        public SignUpVM()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            Creds = new SignUpCreds();
        }

        [RelayCommand]
        async Task SignUp(SignUpCreds creds)
        {
            var json = JsonConvert.SerializeObject(creds);
            var content = new StringContent(json,Encoding.UTF8,"application/json");
            var response = await client.PostAsync("/api/Cafeteria/signup", content);
            if (response.IsSuccessStatusCode)
            {
                await Shell.Current.GoToAsync(nameof(Login));
            }

        }
        [RelayCommand]
        async Task GotoLogin()
        {
            await Shell.Current.GoToAsync(nameof(Login));

        }
    }
}
