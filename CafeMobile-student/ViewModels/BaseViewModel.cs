
using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CafeMobile.ViewModels
{
    public partial class BaseViewModel:ObservableObject
    {
        public HttpClient client;
        private string baseUrl = "https://dcqv9t8r-7276.euw.devtunnels.ms/";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        public bool isBusy = true;
        public bool IsNotBusy => !IsBusy;
        public BaseViewModel()
        {
            
        }
        public void CreateClient()
        {
            client = new();
            client.BaseAddress = new Uri(baseUrl);
        }
        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task GoToRedemptions()
        {
            await Shell.Current.GoToAsync(nameof(Redemptions));
        }
    }
}
