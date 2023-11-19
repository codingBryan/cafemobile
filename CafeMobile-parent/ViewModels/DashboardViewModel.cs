using CafeMobile_parent.Models;
using CafeMobile_parent.Models.DTO;
using CafeMobile_parent.Models.System;
using CafeMobile_parent.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Text;

namespace CafeMobile_parent.ViewModels
{
    [QueryProperty(nameof(Parent),"parent")]
    public partial class DashboardViewModel:BaseViewModel
    {
        [ObservableProperty]
        public ObservableCollection<StudentInfo> myStudents;

        [ObservableProperty]
        public string searchStudentId;

        [ObservableProperty]
        public StudentInfo foundStudent;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(StudentNotFound))]
        public bool studentFound=false;

        public bool StudentNotFound => !StudentFound;

        [ObservableProperty]
        public bool flyOutIsVisible = false;

        [ObservableProperty]
        public bool processing = false;

    



        [ObservableProperty]
        public bool studentSearchIsVisible = false;
        [ObservableProperty]
        public ParentInfo parent;

 
        public DashboardViewModel()
        {
            FoundStudent = new();
        }

        public async Task FetchStudents()
        {
            CreateClient();
            var jwtToken = Preferences.Get("token", "defaultValue");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var response = await client.GetAsync("/api/parent/fetchStudents");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<ObservableCollection<StudentInfo>> data = JsonConvert.DeserializeObject<Response<ObservableCollection<StudentInfo>>>(response_string);
                MyStudents = data.data;
                
            }
        }

        [RelayCommand]
        async Task goToChildTrack(StudentInfo student)
        {
            await Shell.Current.GoToAsync(nameof(ChildTrack),
                new Dictionary<string, object>
                {
                    ["student"] = student
                });
        }
        [RelayCommand]
        async Task ToggleFlyOutVisibility()
        {
           FlyOutIsVisible = !FlyOutIsVisible;
        }
        [RelayCommand]
        async Task ToggleStudentSearchVisibility()
        {
            StudentSearchIsVisible=!StudentSearchIsVisible;
            StudentFound = false;
        }

        [RelayCommand]
        async Task SearchStudent()
        {
            if (SearchStudentId != "")
            {
                CreateClient();
                var response = await client.GetAsync($"/api/parent/searchstudent?reg={SearchStudentId}");
                if (response.IsSuccessStatusCode)
                {
                    var response_string = await response.Content.ReadAsStringAsync();
                    Response<StudentInfo> data = JsonConvert.DeserializeObject<Response<StudentInfo>>(response_string);
                    if (data.data != null)
                    {
                        FoundStudent = data.data;
                        StudentFound = true;
                    }
                
                }
            }
        }
        [RelayCommand]
        async Task AddStudent()
        {
            CreateClient();
            var jwtToken = Preferences.Get("token", "defaultValue");
            var json = JsonConvert.SerializeObject(new Dictionary<string, string> { ["id"] = (FoundStudent.StudentId).ToString()});
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var response = await client.PostAsync("/api/parent/addstudent",content);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<StudentInfo> data = JsonConvert.DeserializeObject<Response<StudentInfo>>(response_string);
                if (data.data != null)
                {
                    StudentSearchIsVisible = !StudentSearchIsVisible;
                    MyStudents.Add(data.data);
                    StudentSearchIsVisible = false;
                }
            }
        }
    }
}
