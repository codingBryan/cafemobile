using CafeMobile_parent.Pages;
using CommunityToolkit.Mvvm.Input;


namespace CafeMobile_parent.ViewModels
{
    public partial class MainPageViewModel:BaseViewModel
    {
        [RelayCommand]
        async Task gotoLogin()
        {
            await Shell.Current.GoToAsync(nameof(SignIn));
        }
    }

    
}
