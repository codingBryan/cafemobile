using CafeMobile_parent.Models;
using CafeMobile_parent.Models.DTO;
using CafeMobile_parent.Models.System;
using CafeMobile_parent.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace CafeMobile_parent.ViewModels
{
    [QueryProperty(nameof(Student),"student")]
    public partial class ChildTrackViewModel:BaseViewModel
    {
        [ObservableProperty]
        public StudentInfo student;
        [ObservableProperty]
        public ChartEntry[] studentSpendingData = new ChartEntry[]
        {
            new ChartEntry(40){
                Label="mon",
                ValueLabel="40",
                ValueLabelColor=SKColor.Parse("#C03840"),
                Color=SKColor.Parse("#38C0B8")
            },
            new ChartEntry(13){
                Label="tue",
                ValueLabel="13",
                ValueLabelColor=SKColor.Parse("#C03840"),
                Color=SKColor.Parse("#38C0B8")
            },
            new ChartEntry(51){
                Label="wed",
                ValueLabel="51",
                ValueLabelColor=SKColor.Parse("#C03840"),
                Color=SKColor.Parse("#38C0B8")
            },
            new ChartEntry(5){
                Label="thur",
                ValueLabel="5",
                ValueLabelColor=SKColor.Parse("#C03840"),
                Color=SKColor.Parse("#38C0B8")
            },
            new ChartEntry(0){
                Label="fri",
                ValueLabel="40",
                ValueLabelColor=SKColor.Parse("#C03840"),
                Color=SKColor.Parse("#38C0B8")
            },
        };
        [ObservableProperty]
        public ObservableCollection<StudentCoupon> coupons;

        [ObservableProperty]
        public int couponCount;

        [ObservableProperty]
        public ObservableCollection<StudentPurchases> redemptions;


        public ChildTrackViewModel()
        {
        }
        [RelayCommand]
        public async Task Init()
        {
            GetCoupons();
            GetRedemptions();
        }

        public async Task GetSpendingData()
        {
            CreateClient();
            var response = await client.GetAsync("/api/parent/fetchStudentExpenditure");
            if (response.IsSuccessStatusCode)
            {
                return;

            }
        }
        public async Task GetCoupons()
        {
            CreateClient();
            var response = await client.GetAsync("/api/parent/fetchStudentCoupons/?id=" + Student.StudentId);
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                Response<ObservableCollection<StudentCoupon>> data = JsonConvert.DeserializeObject<Response<ObservableCollection<StudentCoupon>>>(response_string);
                Coupons = data.data;
                CouponCount = Coupons.Count();

            }
        }

        public async Task GetRedemptions()
        {
            CreateClient();
            var response = await client.GetAsync("/api/parent/fetchStudentRedemptions/?id=" + Student.StudentId);
            if (response.IsSuccessStatusCode)
            {

                var response_string = await response.Content.ReadAsStringAsync();
                Response<ObservableCollection<StudentPurchases>> data = JsonConvert.DeserializeObject<Response<ObservableCollection<StudentPurchases>>>(response_string);
                Redemptions = data.data;

            }
        }


        [RelayCommand]
        async Task gotoBuyCoupons()
        {
            await Shell.Current.GoToAsync(nameof(TopUpCP),
                new Dictionary<string, object>
                {
                    ["student"] = Student
                });
        }

        [RelayCommand]
        async Task goToCoupons()
        {
            await Shell.Current.GoToAsync(nameof(Coupons), new Dictionary<string, object>
            {
                ["student"] = Student
            });
        }
    }
}
