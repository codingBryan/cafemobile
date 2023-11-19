using CafeMobile.Models;
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace CafeMobile.ViewModels.StudentVms
{
    public partial class StudentLoginViewModel:BaseViewModel
    {
        [ObservableProperty]
        public LoginDTO loginDetails;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(credsSent))]
        public bool credsNotSent = true;
        public bool credsSent => !CredsNotSent;
        public StudentLoginViewModel()
        {
            loginDetails = new LoginDTO();
        }

        [RelayCommand]
        async Task Login(LoginDTO creds)
        {
            CreateClient();
            CredsNotSent = false;
            Response<Student> res = new();
            var json = JsonConvert.SerializeObject(creds);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/Student/login", content);
            if (response.IsSuccessStatusCode)
            {
                CredsNotSent = true;
                var response_string = await response.Content.ReadAsStringAsync();
                Token<StudentInfo> studentDetails = new();
                Response<Token<StudentInfo>> data = JsonConvert.DeserializeObject<Response<Token<StudentInfo>>>(response_string);
                if (data.data.user != null) 
                {
                    Preferences.Set("token", data.data.token);
                    await Shell.Current.GoToAsync(nameof(StudentMenu), new Dictionary<string, object>
                    {
                        ["student"] = data.data.user
                    }) ;
                }
            }
        }
        [RelayCommand]
        async Task gotoSignUp()
        {
            await Shell.Current.GoToAsync(nameof(StudentSignUp));
        }
    }
}
