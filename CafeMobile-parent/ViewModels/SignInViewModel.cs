using CafeMobile_parent.Models.DTO;
using CafeMobile_parent.Models.System;
using CafeMobile_parent.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace CafeMobile_parent.ViewModels
{
    public partial class SignInViewModel:BaseViewModel
    {
        [ObservableProperty]
        public LoginDTO loginDetails;

        public SignInViewModel()
        {
            loginDetails = new();
        }

        [RelayCommand]
        async Task Login(LoginDTO creds)
        {
            CredsNotSent = false;
            CreateClient();
            var json = JsonConvert.SerializeObject(creds);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/parent/login", content);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Token<ParentInfo> parentDetails = new();
                Response<Token<ParentInfo>> data = JsonConvert.DeserializeObject<Response<Token<ParentInfo>>>(response_string);
                if (data.data.user != null)
                {
                    Preferences.Set("token", data.data.token);

                    await Shell.Current.GoToAsync(nameof(Dashboard), new Dictionary<string, object>
                    {
                        ["parent"] = data.data.user
                    });
                }
                CredsNotSent = true;
            }
        }
        [RelayCommand]
        async Task GotoSignUp()
        {
            CredsNotSent = false;
            await Shell.Current.GoToAsync(nameof(SignUp));
            CredsNotSent = true;
        }
    }
}
