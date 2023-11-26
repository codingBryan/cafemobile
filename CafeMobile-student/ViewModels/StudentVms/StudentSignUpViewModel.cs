using CafeMobile.Models;
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace CafeMobile.ViewModels.StudentVms
{
    public partial class StudentSignUpViewModel:BaseViewModel
    {
        [ObservableProperty]
        public StudentCredentials creds;
        private readonly IMapper mapper;

        public StudentSignUpViewModel(IMapper mapper)
        {
            creds = new StudentCredentials();
            this.mapper = mapper;
        }

        [RelayCommand]
        async Task SignUp(StudentCredentials creds)
        {
            CreateClient();
            if (creds.password == creds.passwordConfirm)
            {
                Student student = new Student()
                {
                    first_name=creds.first_name,
                    last_name=creds.last_name,
                    email=creds.email,
                    phone_number = creds.phone_number,
                    adm_no=creds.adm_no,
                    password=creds.password,
                };
                Response<Student> res = new();
                var json = JsonConvert.SerializeObject(student);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("/api/student/signup", content);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    res = JsonConvert.DeserializeObject<Response<Student>>(responseString);
                }
                if (res.success)
                {
                    StudentInfo s = mapper.Map<StudentInfo>(res.data);
                    await Shell.Current.GoToAsync(nameof(StudentLogin));
                    
                }
            }
        }
        [RelayCommand]
        async Task gotoMenu()
        {
            await Shell.Current.GoToAsync(nameof(StudentMenu));
        }
    }
}
