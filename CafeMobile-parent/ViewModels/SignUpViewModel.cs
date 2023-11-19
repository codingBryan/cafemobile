using CafeMobile_parent.Models.DTO;
using CafeMobile_parent.Models.System;
using CafeMobile_parent.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace CafeMobile_parent.ViewModels
{
    public partial class SignUpViewModel:BaseViewModel
    {
        [ObservableProperty]
        public NewParentDTO newParent;

        [ObservableProperty]
        public string confirmPassword;
        public SignUpViewModel()
        {
            newParent = new();
        }
        [RelayCommand]
        async Task GotoSignIn()
        {
            await Shell.Current.GoToAsync(nameof(SignIn));
        }

        [RelayCommand]
        async Task SignUp(NewParentDTO parent)
        {
            CredsNotSent = false;
            CreateClient();
            var json = JsonConvert.SerializeObject(parent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/parent/signup", content);
            if (response.IsSuccessStatusCode)
            {
                CredsNotSent = true;
                var response_string = await response.Content.ReadAsStringAsync();
                Response<ParentInfo> data = JsonConvert.DeserializeObject<Response<ParentInfo>>(response_string);
                if (data.data != null)
                {

                    await Shell.Current.GoToAsync(nameof(SignIn));
                }
            }
        }
    }
}
