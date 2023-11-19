using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Cafemobile_Cafeteria.ViewModels
{
    public partial class BaseViewModel:ObservableObject
    {
        public HttpClient client;
        private string baseUrl = "https://dcqv9t8r-7276.euw.devtunnels.ms/";


        public BaseViewModel()
        {
        }
        [ObservableProperty]
        public bool isBusy = true;

        [RelayCommand]

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
    }
}
