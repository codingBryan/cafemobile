using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CafeMobile_parent.ViewModels
{
    public partial class BaseViewModel:ObservableObject
    {
        public HttpClient client;
        private string BaseUrl = "https://dcqv9t8r-7276.euw.devtunnels.ms/";
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(credsSent))]
        public bool credsNotSent = true;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotRefresh))]
        public bool isRefresh = false;

        public bool IsNotRefresh => !isRefresh;
        public bool credsSent => !CredsNotSent;

        public void CreateClient()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseUrl);
        }
        [RelayCommand]
        async Task goBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
