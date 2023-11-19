using CafeMobile.Models.DTO;
using Cafemobile_Cafeteria.Models.DTO;
using Cafemobile_Cafeteria.Models.System;
using Cafemobile_Cafeteria.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microcharts;
using Newtonsoft.Json;
using SkiaSharp;
using System.Collections.ObjectModel;

namespace Cafemobile_Cafeteria.ViewModels
{
    [QueryProperty(nameof(Staff),"staff")]
    public partial class DashboardVM:BaseViewModel
    {

        [ObservableProperty]
        public StaffInfo staff;
        [ObservableProperty]
        public ChartEntry[] sales_chart_data = new ChartEntry[]
        {
            new ChartEntry(3100)
            {
                Label="7am",
                ValueLabel="3100",
                ValueLabelColor=SKColor.Parse("#C03840"),
                Color=SKColor.Parse("#38C0B8")
            },
             new ChartEntry(5000)
            {
                Label="8am",
                ValueLabelColor=SKColor.Parse("#C03840"),
                ValueLabel="5000",
                Color=SKColor.Parse("#38C0B8")
            },
              new ChartEntry(1200)
            {
                Label="9am",
                ValueLabelColor=SKColor.Parse("#C03840"),
                ValueLabel = "1200",
                Color=SKColor.Parse("#38C0B8")

            },
              new ChartEntry(1230)
            {
                Label="10am",
                ValueLabelColor=SKColor.Parse("#C03840"),
                ValueLabel="1100",
                Color=SKColor.Parse("#38C0B8")
            },
             new ChartEntry(6000)
            {
                Label="10am",
                ValueLabelColor=SKColor.Parse("#C03840"),
                ValueLabel="6000",
                Color=SKColor.Parse("#38C0B8")
            },
              new ChartEntry(1000)
            {
                Label="12am",
                ValueLabelColor=SKColor.Parse("#C03840"),
                ValueLabel="1000",
                Color=SKColor.Parse("#38C0B8")
            },
        };

        [ObservableProperty]
        public ChartEntry[] customer_traffic_data = new ChartEntry[]
        {
            new ChartEntry(22)
            {
                Label="Visitors",
                ValueLabel="22",
                Color = SKColor.Parse("#C03840")
            },
            new ChartEntry(30)
            {
                Label="Lecturers",
                ValueLabel="30",
                Color = SKColor.Parse("#C03840")
            },
            new ChartEntry(128)
            {
                Label="Students",
                ValueLabel="128",
                Color = SKColor.Parse("#38C0B8")
            },


        };

        [ObservableProperty]
        public ChartEntry[] payment_method_data = new ChartEntry[]
        {
            new ChartEntry(22)
            {
                Label="M-Pesa",
                ValueLabel="22",
                Color = SKColor.Parse("#38C077")
            },
            new ChartEntry(40)
            {
                Label="CP",
                ValueLabel="30",
                Color = SKColor.Parse("#C03840")
            },
            new ChartEntry(30)
            {
                Label="Cash",
                ValueLabel="128",
                Color = SKColor.Parse("#38C0B8")
            },
        };

        [ObservableProperty]
        public bool flyoutIsVisible = false;

        [ObservableProperty]
        public ObservableCollection<ItemSale> salesToday=new();


        public DashboardVM()
        {
            
        }
        public async void Init()
        {
            FetchTodaySales();
        }

        [RelayCommand]
        async Task ToggleFlyOutVisibility()
        {
            FlyoutIsVisible = !FlyoutIsVisible;
        }

        [RelayCommand]
        async Task GoToScan()
        {
            FlyoutIsVisible = !FlyoutIsVisible;
            await Shell.Current.GoToAsync(nameof(ScanQR));
        }

        [RelayCommand]
        async Task GoToMeals()
        {
            FlyoutIsVisible = !FlyoutIsVisible;
            await Shell.Current.GoToAsync(nameof(Meals));
        }

        [RelayCommand]
        async Task GoToCoupons()
        {
            FlyoutIsVisible = !FlyoutIsVisible;
            await Shell.Current.GoToAsync(nameof(Coupons));
        }

        [RelayCommand]
        async Task GoToRedeems()
        {
            FlyoutIsVisible = !FlyoutIsVisible;
            await Shell.Current.GoToAsync(nameof(Redeems));
        }
        private async void FetchTodaySales()
        {
            CreateClient();
            var response = await client.GetAsync("api/cafeteria/todaySales");
            if (response.IsSuccessStatusCode)
            {
                var response_string = await response.Content.ReadAsStringAsync();
                var response_data = JsonConvert.DeserializeObject<Response<ObservableCollection<ItemSale>>>(response_string);
                ObservableCollection<ItemSale> sales = response_data.data;
                SalesToday = sales;
            }
        }
    }
}
