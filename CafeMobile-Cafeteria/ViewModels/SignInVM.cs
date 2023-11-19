using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class SignInVM:BaseViewModel
    {
        [ObservableProperty]
        public LoginData creds;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(credsSent))]
        public bool credsNotSent=true;

        public bool credsSent => !CredsNotSent;
        public SignInVM()
        {
            Creds = new();
        }
        [RelayCommand]
        async Task Login(LoginData creds)
        {
            CredsNotSent = false;
            CreateClient();
            var json = JsonConvert.SerializeObject(creds);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("api/cafeteria/login", content);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Token<StaffInfo> staffDetails = new();
                Response<Token<StaffInfo>> data = JsonConvert.DeserializeObject<Response<Token<StaffInfo>>>(response_string);
                if (data.data.user != null)
                {
                    Preferences.Set("token", data.data.token);

                    await Shell.Current.GoToAsync(nameof(DashBoard), new Dictionary<string, object>
                    {
                        ["staff"] = data.data.user
                    });

                    CredsNotSent = true;
                }

            }
            CredsNotSent = true;
        }
        [RelayCommand]
        async Task GotoSignUp()
        {
            await Shell.Current.GoToAsync(nameof(signUp));
        }
    }
}
