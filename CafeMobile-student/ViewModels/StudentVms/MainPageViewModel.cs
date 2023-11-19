using CafeMobile.Pages.Student;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CafeMobile.ViewModels
{
    public partial class MainPageViewModel:ObservableObject
    {
        
        [RelayCommand]
        async Task goToLogin()
        {
            await Shell.Current.GoToAsync(nameof(StudentLogin));
        }
    }
}
