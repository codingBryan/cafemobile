using CafeMobile_parent.Models.DTO;
using CafeMobile_parent.Models.System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Text;

namespace CafeMobile_parent.ViewModels
{
    [QueryProperty(nameof(Student),"student")]
    public partial class TopUpCpViewModel:BaseViewModel
    {
        [ObservableProperty]
        public StudentInfo student;

        [ObservableProperty]
        public int amount;

        [ObservableProperty]
        public double cp_recieve;


        public TopUpCpViewModel()
        {
            
        }
        [RelayCommand]
        async Task BuyCP()
        {
            CreateClient();
            var jwtToken = Preferences.Get("token", "defaultValue");
            BuyCP cpb = new()
            {
                studentId = Student.StudentId,
                amount_cp = Amount,
                transactionCode = "codexxx",
                amount_ksh = Amount * 10

            };
            var json = JsonConvert.SerializeObject(cpb);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            client.DefaultRequestHeaders.Add("Authorization", $"bearer {jwtToken}");
            var response = await client.PostAsync("/api/parent/purchaseCp", content);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<string> data = JsonConvert.DeserializeObject<Response<string>>(response_string);
                if (data.success == true)
                {
                    await Shell.Current.DisplayAlert("Success", "Top up was successfull", "OK");
                }
            }
        }
    }
}
